using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HelloService
{

    public class ApiHost
    {
        string baseUri = "http://localhost:9000/";
        public void Run()
        {
            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseUri))
            {
                Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
                Console.ReadLine();
            }
        }
    }


    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }
    }
}
