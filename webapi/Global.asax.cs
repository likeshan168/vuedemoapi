using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace webapi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private const string WebApiPrefix = "api";
        private static string WebApiExecutePath = string.Format("~/{0}", WebApiPrefix);
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
        private bool IsWebAPiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiExecutePath);
        }
        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebAPiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
            }
        }

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    var req = HttpContext.Current.Request;
        //    if (req.HttpMethod == "OPTIONS")//过滤options请求，用于js跨域
        //    {
        //        Response.StatusCode = 200;
        //        Response.SubStatusCode = 200;
        //        Response.End();
        //    }
        //}
    }
}
