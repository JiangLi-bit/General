using System;
using System.Collections.Generic;
using General.Entity;
using System.Linq;
using General.Core;
using Microsoft.Extensions.Caching.Memory;
using General.IService;

namespace General.Service
{
    public class CategoryService : ICategoryService
    {
        private const string MODEL_KEY = "General.services.category";

        private IMemoryCache _memoryCache;
        private IRepositoryBase<Entity.Category> _categoryRepository;
        private IRepositoryBase<Entity.SysPermission> _permissionRepository;

        public CategoryService(IRepositoryBase<Entity.Category> categoryRepository,
            IMemoryCache memoryCache,
            IRepositoryBase<Entity.SysPermission> permissionRepository)
        {
            this._permissionRepository = permissionRepository;
            this._memoryCache = memoryCache;
            this._categoryRepository = categoryRepository;
        }

        /// <summary>
        /// 初始化保存方法
        /// </summary>
        /// <param name="list"></param>
        public void initCategory(List<Category> list)
        {
            var oldList = _categoryRepository.Table.ToList();
            oldList.ForEach(del =>
            {
                var item = list.FirstOrDefault(o => o.SysResource == del.SysResource);
                if (item == null)
                {
                    var permissionList = del.SysPermissions.ToList();
                    permissionList.ForEach(delPrm =>
                    {
                        _permissionRepository.Entities.Remove(delPrm);
                    });
                    _categoryRepository.Entities.Remove(del);
                }
            });
            list.ForEach(entity =>
            {
                var item = oldList.FirstOrDefault(o => o.SysResource == entity.SysResource);
                if (item == null)
                {
                    _categoryRepository.Entities.Add(entity);
                }
                else
                {
                    item.Action = entity.Action;
                    item.Controller = entity.Controller;
                    item.CssClass = entity.CssClass;
                    item.FatherResource = entity.FatherResource;
                    item.IsMenu = entity.IsMenu;
                    item.Name = entity.Name;
                    item.RouteName = entity.RouteName;
                    item.SysResource = entity.SysResource;
                    item.Sort = entity.Sort;
                    item.FatherID = entity.FatherID;
                    item.ResouceID = entity.ResouceID;
                }
            });
            if (_categoryRepository.DbContext.ChangeTracker.HasChanges())
                _categoryRepository.DbContext.SaveChanges();
        }

        /// <summary>
        /// 获取所有的菜单数据 并缓存
        /// </summary>
        /// <returns></returns>
        public List<Category> getAll()
        {
            List<Category> list = null;
            _memoryCache.TryGetValue<List<Category>>(MODEL_KEY, out list);
            if (list != null)
                return list;
            list = _categoryRepository.Table.ToList();
            _memoryCache.Set(MODEL_KEY, list, DateTimeOffset.Now.AddDays(1));
            return list;
        }
    }
}
