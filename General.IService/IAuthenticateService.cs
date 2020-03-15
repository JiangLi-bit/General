using System;
using System.Collections.Generic;
using System.Text;

namespace General.IService
{
    public interface IAuthenticateService
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        bool IsAuthenticated(string tokenId, out string token);
    }
}
