using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Security;
using lks.webapi.Utility;

namespace lks.webapi.OAuth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class FormAuth : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                base.OnActionExecuting(actionContext);
                #region 通过cookie form的方式进行验证，不能跨域使用
                //if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0)
                //{
                //    base.OnActionExecuting(actionContext);
                //    return;
                //}
                ////在跨域访问的时候，我们不能通过cookie来验证，因为跨域是不能访问到cookie的
                //var cookie = actionContext.Request.Headers.GetCookies();
                ////var querystr = actionContext.Request.GetQueryNameValuePairs();
                //if (cookie == null || cookie.Count < 1)
                //{
                //    actionContext.Response = new HttpResponseMessage();
                //    actionContext.Response.Content = new StringContent("{\"msg\":\"未登录\",\"code\":403}");
                //    return;
                //}
                //FormsAuthenticationTicket ticket = null;
                //foreach (var perCookie in cookie[0].Cookies)
                //{
                //    if (perCookie.Name == FormsAuthentication.FormsCookieName)
                //    {
                //        ticket = FormsAuthentication.Decrypt(perCookie.Value);
                //        break;
                //    }
                //}
                //if (ticket == null)
                //{
                //    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                //    return;
                //}

                //// TODO: 添加其它验证方法
                //base.OnActionExecuting(actionContext); 
                #endregion

                #region query string
                if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0)
                {
                    base.OnActionExecuting(actionContext);
                    return;
                }
                var context = actionContext.Request.Properties["MS_HttpContext"] as HttpContextBase;

                string queryStr = string.Empty;
                if (context.Request.UrlReferrer != null)
                {
                    queryStr = context.Request.UrlReferrer.Query;
                }
                else
                {
                    queryStr = context.Request.Url.Query;
                }

                //var rst =  actionContext.Request.Content.ReadAsStreamAsync();
                //rst.Wait();

                //var task = actionContext.Request.Content.ReadAsStreamAsync();
                //var cont = string.Empty;
                //using (Stream sm = task.Result)
                //{
                //    if (sm != null)
                //    {
                //        sm.Seek(0, SeekOrigin.Begin);
                //        int len = (int)sm.Length;
                //        byte[] inputByts = new byte[len];
                //        sm.Read(inputByts, 0, len);
                //        sm.Close();
                //        cont = Encoding.UTF8.GetString(inputByts);
                //    }
                //}

                //var a = content.Request.QueryString["a"];
                //var querystr = actionContext.Request.GetQueryNameValuePairs();
                if (queryStr == null)
                {
                    actionContext.Response = new HttpResponseMessage();
                    actionContext.Response.Content = new StringContent("{\"msg\":\"未登录\",\"code\":403}");
                    return;
                }
                string ticket = string.Empty;
                var queryList = QuerySringHelper.ResolveQuery(queryStr);
                ticket = queryList["a"];
                string name = GetUserName(ticket);

                //从缓存中取出token
                object val = MemoryCacher.GetValue(name);
                string token = val == null ? string.Empty : val.ToString();
                //将客户端传过来的ticket与缓存中的token进行比较
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ticket) || string.IsNullOrWhiteSpace(token) || ticket != token)
                {
                    actionContext.Response = new HttpResponseMessage();
                    actionContext.Response.Content = new StringContent("{\"msg\":\"未登录\",\"code\":403}");
                    return;
                }

                #endregion
            }
            catch (Exception ex)
            {
                actionContext.Response = new HttpResponseMessage();
                actionContext.Response.Content = new StringContent("{\"msg\":\"{" + ex.Message + "}\",\"code\":403}");
            }
        }

        private static string GetUserName(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
                return string.Empty;

            string userData = FormsAuthentication.Decrypt(ticket).UserData;
            string[] users = userData.Split('#');
            if (users.Length != 0)
            {
                return users[0];
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
