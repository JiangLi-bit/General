using System.Collections.Generic;
using General.Entity;
using General.Framework.Security;

namespace General.Framework
{
    public class WorkContext : IWorkContext
    {
        private ILoginAuthService _authenticationService;

        public WorkContext(ILoginAuthService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public SysUser CurrentUser
        {
            get { return _authenticationService.getCurrentUser(); }
        }

        /// <summary>
        /// 当前登录用户菜单
        /// </summary>
        public List<Category> Categories {
            get
            {
              return _authenticationService.getMyCategories();
            }
        }
    }
}
