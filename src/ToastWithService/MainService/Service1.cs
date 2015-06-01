using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HelloService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        public void Start(string[] args) { OnStart(args); }

        protected override void OnStart(string[] args)
        {
            var host = new ApiHost();
            host.Run();
        }

        protected override void OnStop()
        {
        }
    }
}
