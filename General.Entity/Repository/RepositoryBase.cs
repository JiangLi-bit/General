﻿using General.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace General.Entity
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private GeneralDbContext _dbContext;

        public RepositoryBase(GeneralDbContext generalDbContext)
        {
            this._dbContext = generalDbContext;
        }

        public DbContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<TEntity> Entities
        {
            get
            {
                return _dbContext.Set<TEntity>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IQueryable<TEntity> Table
        {
            get
            {
                return Entities;
            }
        }

        public void delete(TEntity entity, bool isSave = true)
        {
            Entities.Remove(entity);
            if (isSave)
                _dbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity getById(object id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void insert(TEntity entity, bool isSave = true)
        {
            Entities.Add(entity);
            if (isSave)
                _dbContext.SaveChanges();
        }

        public void update(TEntity entity, bool isSave = true)
        {
            if (isSave)
                _dbContext.SaveChanges();
        }
    }
}