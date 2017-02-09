using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using owinapp;

namespace owinhost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StartOptions options = new StartOptions();
                //服务器Url设置
                options.Urls.Add("http://localhost:5000");
                options.Urls.Add("http://localhost:5001");
                //Server实现类库设置,下面的代码可以不写，默认就是HttpListener
                options.ServerFactory= "Microsoft.Owin.Host.HttpListener";

                using (WebApp.Start<webapi.WebAPIStartup>(options))
                {
                    Console.WriteLine("press [enter] to esc");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
