using Microsoft.Owin.Hosting;
using Microsoft.Owin;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Configuration;

namespace HelloService
{

    public class ApiHost
    {
        bool _isRunning = true;
        IDisposable _server = null;
        int _port = 9000;
        string _baseUri;

        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; }
        }

        public ApiHost()
        {
            try
            {
                var port = ConfigurationManager.AppSettings["apihost:port"];
                int.TryParse(port, out _port);
                _baseUri = string.Format("http://localhost:{0}/", _port);
            }
            catch { }
        }

        public IDisposable Run(bool interactive = false)
        {
            if (!interactive)
            {
                _server = WebApp.Start<Startup>(url: _baseUri);
            }
            else
            {
                // Start OWIN host 
                using (WebApp.Start<Startup>(url: _baseUri))
                {
                    while (_isRunning)
                    {
                        Debug.WriteLine("Server running at {0} - press Enter to quit. ", _baseUri);
                        Console.WriteLine("Server running at {0} - press Enter to quit. ", _baseUri);
                        Console.ReadLine();
                    }
                }
            }
            return _server;
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
