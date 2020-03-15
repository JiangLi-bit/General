using General.Core;
using General.Framework.Filters;

namespace General.Framework.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AdminAuthFilter] 
    public class PublicController : LoginAreaController
    {
        private IWorkContext _workContext;

        public PublicController()
        {
            this._workContext = EnginContext.Current.Resolve<IWorkContext>();
        }

        /// <summary>
        /// 当前工作上下文
        /// </summary>
        public IWorkContext WorkContext { get { return _workContext; } }
    }


}
