using Fileshare.Logics.FileShareManager;
using FileShare.Contracts.FileShareServices;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace FileShare.Desktop.FileShareServices.FileShareHostService
{
    public class FIleShareHostService : IFileShareHostService
    {
        private ServiceHost serviceHost;

        public int Port { get; }
        public string Uri { get; }
        public bool IsStarted { get; set; }

        public FIleShareHostService(int port, string uri)
        {
            Port = port;
            Uri = uri;
            IsStarted = false;
        }

        public bool Start()
        {
            Uri[] uri =  new Uri[1];
            if(!string.IsNullOrEmpty(Uri) && Port > 0)
            {
                string address = $"net.tcp://{Uri}:{Port}/FileShare";
                uri[0] = new Uri(address);
                IFileShareService fileShare = new FileShareManager();
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);

                serviceHost = new ServiceHost(fileShare, uri);
                serviceHost.AddServiceEndpoint(typeof(IFileShareService), binding, "");
                serviceHost.Opened += HostOnOpened;
                serviceHost.Open();
            }
            return IsStarted;
        }

        public bool Stop()
        {
            if(serviceHost != null)
            {
                serviceHost.Closed += HostOnClosed;
                serviceHost.Close();
                serviceHost = null;
            }
            return IsStarted;
        }

        private void HostOnOpened(object sender, EventArgs eventArgs)
        {
            IsStarted = true;
        }

        private void HostOnClosed(object sender, EventArgs eventArgs)
        {
            IsStarted = false;
        }
    }
}
