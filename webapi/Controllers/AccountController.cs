using System.Web;
using System.Web.Http;
using lks.webapi.BLL;
using lks.webapi.Model;
using lks.webapi.OAuth;

namespace webapi.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly UserInfoService _bllService = new UserInfoService();
        private readonly RoleService _roleService = new RoleService();
        private readonly RouteService _routeService = new RouteService();
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
        [Route("getList"), HttpPost, FormAuth]
        public dynamic GetUserList(PagePara para)
        {
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
            HttpRequestBase request = context.Request;//定义传统request对象
            string queryStr = string.Empty;
            if (context.Request.UrlReferrer != null)
            {
                queryStr = context.Request.UrlReferrer.Query;
            }
            else
            {
                queryStr = context.Request.Url.Query;
            }
            int total = 0;
            return _bllService.GetList(queryStr, para.Columns, para.Index, para.Size, para.WhereStr, para.OrderField, out total);
        }
        [Route("getRoleList"), HttpPost]
        public dynamic GetRoleList(PagePara para)
        {
            int total = 0;
            if (para.Other)
                return _roleService.QueryList3(para.Columns, para.Index, para.Size, para.WhereStr, para.OrderField, out total);
            else
                return _roleService.QueryList2(para.Columns, para.Index, para.Size, para.WhereStr, para.OrderField, out total);
        }
        [Route("getRouteList"), HttpPost]
        public dynamic GetRouteList(PagePara para)
        {
            int total = 0;
            return _routeService.QueryList2(para.Columns, para.Index, para.Size, para.WhereStr, para.OrderField, out total);
        }
        [Route("getRouteByRoleId"), HttpGet]
        public dynamic GetRouteByRoleId(int roleId)
        {
            return _routeService.GetRouteByRoleId(roleId);
        }

        [Route("addRoute"), HttpPost]
        public dynamic AddRoute(Route route)
        {
            return _routeService.AddModel(route);
        }

        [Route("updateRoute"), HttpPost]
        public dynamic UpdateRoute(Route route)
        {
            return _routeService.Update(route);
        }
        [Route("deleteRoute"), HttpPost]
        public dynamic DeleteRoute(Route route)
        {
            return _routeService.Delete(route.RouteId);
        }

        [Route("addRole"), HttpPost]
        public dynamic AddRole(Role role)
        {
            return _roleService.AddModel(role);
        }

        [Route("updateRole"), HttpPost]
        public dynamic UpdateRole(Role role)
        {
            return _roleService.Update(role);
        }
        [Route("deleteRoute"), HttpPost]
        public dynamic DeleteRole(Role role)
        {
            return _roleService.Delete(role.RoleId);
        }
    }
}
