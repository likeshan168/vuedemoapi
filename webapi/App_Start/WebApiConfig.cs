﻿using System.Web.Http;
using System.Web.Http.Cors;
using lks.webapi.Utility;

namespace webapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Replace default JSON serializer
            config.Formatters.RemoveAt(0);
            config.Formatters.Insert(0, new JilFormatter());

            // Web API configuration and services
            // 解决json序列化时的循环引用问题
            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //// 对 JSON 数据使用混合大小写。驼峰式,但是是javascript 首字母小写形式.
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new  CamelCasePropertyNamesContractResolver();
            // 对 JSON 数据使用混合大小写。跟属性名同样的大小.输出
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver();
            // 使api返回为json 
            //config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            // Web API routes
            config.MapHttpAttributeRoutes();
            //只有这样配置的跨域访问才有效果
            config.EnableCors(new EnableCorsAttribute("http://101.201.28.235:84,http://localhost:8080", "*", "*"));
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
