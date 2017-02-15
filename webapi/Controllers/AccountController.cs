using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using lks.webapi.BLL;
using lks.webapi.Model;

namespace webapi.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly UserInfoService _bllService = new UserInfoService();
        //IOwinContext

        [Route("register"), HttpPost]
        public dynamic Register(UserInfo userInfo)
        {
            return _bllService.AddModel(userInfo);
        }
        [Route("login"), HttpPost]
        public dynamic Login(UserInfo userInfo)
        {
            return _bllService.Exists(userInfo.Name, userInfo.Password);
        }
        [Route("update"), HttpPost]
        public dynamic Update(UserInfo user)
        {
            return _bllService.Update(user);
        }
        [Route("delete"), HttpPost]
        public dynamic Delete(UserInfo user)
        {
            return _bllService.Delete(user.Id);
        }
        [Route("getList"), HttpPost]
        public dynamic GetUserList(PagePara para)
        {
            int total = 0;
            return new
            {
                Users = _bllService.QueryList(null, para.Index, para.Size, para.WhereStr, para.OrderField, out total),
                Total = total
            };
        }
    }
}
