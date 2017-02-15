using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using lks.webapi.Attribute;
using lks.webapi.BLL;
using lks.webapi.Model;

namespace webapi.Controllers
{
    [RoutePrefix("api")]
    //[CrossSite]
    //[EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
    public class RegisterController : ApiController
    {
        private readonly UserInfoService _bllService = new UserInfoService();

        [Route("register"), HttpPost]
        public dynamic Post(UserInfo userInfo)
        {
           return  _bllService.AddModel(userInfo);
        }
    }
}
