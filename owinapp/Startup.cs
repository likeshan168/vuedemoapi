using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(owinapp.Startup))]

namespace owinapp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseErrorPage();
            //app.Run(context =>
            //{
            //    if (context.Request.Path.Value=="/fail")
            //    {
            //        throw new Exception("random exception");
            //    }
            //    context.Response.ContentType = "text/plain";
            //    return context.Response.WriteAsync("hello,world");
            //});

            app.Use<SampleMiddleware>();

            
        }
    }
}
