using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using lks.webapi.Model;
using Newtonsoft.Json;

namespace webapi.Controllers
{
    [SessionValidate]
    public class BasicController : ApiController
    {
    }

    public class SessionValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            //通过owin自主寄宿的话，HttpContext.Current为空
            if (HttpContext.Current.Session["CurrentUser"] == null)
            {

                ResponseResult result = new ResponseResult()
                {
                    Code = 401,
                    Msg = "没有权限访问"
                };
                string content = JsonConvert.SerializeObject(result);
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.StatusCode = 200;
                HttpContext.Current.Response.Write(content);
                HttpContext.Current.Response.End();
            }
        }
    }
   
}
