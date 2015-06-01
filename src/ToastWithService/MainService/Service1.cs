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

        bool _interactive = false;
        IDisposable _server;
        ApiHost _host;

        public bool Interactive
        {
            get { return _interactive; }
            set { _interactive = value; }
        }

        public Service1()
        {
            InitializeComponent();
        }

        public void Start(string[] args) { 
            OnStart(args); 
        }

        protected override void OnStart(string[] args)
        {
            _host = new ApiHost();
            _server = _host.Run(_interactive);
        }

        protected override void OnStop()
        {
            _host.IsRunning = false;
            if (null != _server) 
                _server.Dispose();

        }
    }
}
