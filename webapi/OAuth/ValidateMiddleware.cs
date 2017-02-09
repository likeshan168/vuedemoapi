using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using lks.webapi.Model;
using Microsoft.Owin;
using Newtonsoft.Json;

namespace webapi.OAuth
{
    public class ValidateMiddleware : OwinMiddleware
    {
        public ValidateMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {

            try
            {
                PathString path = new PathString("/api/account/login");
                if (context.Request.Path.StartsWithSegments(path))
                    return Next.Invoke(context);

                if (HttpContext.Current.Session["CurrentUser"] == null)
                {
                    ResponseResult result = new ResponseResult()
                    {
                        Code = 401,
                        Msg = "没有权限访问"
                    };
                    string content = JsonConvert.SerializeObject(result);
                    context.Response.ContentType = "application/json";
                    context.Response.ContentLength = content.Length;
                    context.Response.StatusCode = 200;
                    context.Response.Expires = DateTimeOffset.Now;
                    context.Response.Write(content);
                    //解答者告诉Server解答已经完毕,后续Middleware不需要处理
                    //Task.FromResult(0) 表示一个空的Task,说明该Middleware在某些情况下不再触发后续的Middleware运行—也就是”到此为止”.
                    return Task.FromResult(0);
                }
                else
                {
                    return Next.Invoke(context);
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
                return Task.FromResult(0);
            }
        }
    }
}