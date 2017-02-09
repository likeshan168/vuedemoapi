using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;

namespace owinapp
{
    public class SampleMiddleware : OwinMiddleware
    {
        public SampleMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            PathString tickpath = new PathString("/tick");
            if (context.Request.Path.StartsWithSegments(tickpath))
            {
                string content = DateTime.Now.Ticks.ToString();
                context.Response.ContentType = "text/plain";
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
    }
}