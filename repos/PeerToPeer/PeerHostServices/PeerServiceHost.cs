using Fileshare.Logics.FileShareManager;
using Fileshare.Logics.ServiceManager;
using FileShare.Contracts.FileShareServices;
using FileShare.Contracts.Repository;
using FileShare.Contracts.Services;
using FileShare.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PeerToPeer.PeerHostServices
{
    public class PeerServiceHost
    {
        private AutoResetEvent resetEvent = new AutoResetEvent(false);
        private bool isStarted = false;
        private readonly int port = 0;
        private FileShareManager file = new FileShareManager();
        Dictionary<string, HostInfo> currentHost = new Dictionary<string, HostInfo>();

        public IPeerRegistrationRepository RegistrPeer { get; set; }
        public IPeerNameResolverRepository ResolverPeer { get; set; }
        public IPeerConfigurationService<PingService> ConfigurPeer { get; set; }

        public PeerServiceHost(IPeerRegistrationRepository peerRegistration, IPeerNameResolverRepository peerNameResolver, IPeerConfigurationService<PingService> peerConfiguration)
        {
            RegistrPeer = peerRegistration;
            ResolverPeer = peerNameResolver;
            ConfigurPeer = peerConfiguration;
            port = ConfigurPeer.Port;
        }

        public void RunPeerServiceHost(Peer<IPingService> peer, Action onSuccess)
        {
            if (peer == null)
                throw new ArgumentNullException(nameof(peer));

            RegistrPeer.StartPeerRegistration(peer.Id, port);

            if(RegistrPeer.IsPeerRegistered)
            {
                Console.WriteLine($"{peer.Username} Registration complited");
                Console.WriteLine($"Peer Uri: {RegistrPeer.PeerUri}     Port: {port}");
            }

            if(ResolverPeer != null)
            {
                Console.WriteLine($"Resolving {peer.Username}");
                
                ResolverPeer.ResolvPeerName(peer.Id);
                PeerEndPointsCollection result = ResolverPeer.PeerEndPointCollection;

                Console.WriteLine($"\t\t EndPoints for {RegistrPeer.PeerUri}");
                
                if(ConfigurPeer.StartPeerService())
                {
                    Console.WriteLine("Peer services started");
                    peer.Channel.Ping(new HostInfo()
                    {
                        Id = peer.Id,
                        Port = port,
                        Uri = RegistrPeer.PeerUri
                    });


                    if(ConfigurPeer.PingService != null)
                    {
                        ConfigurPeer.PingService.PeerEndPointInformation += PingServiceOnPeerEndPointInformation;
                    }
                    onSuccess?.Invoke();

                    //Thread thd = new Thread(new ThreadStart(() =>
                  //  {
                        if(StartFileShareService(port, RegistrPeer.PeerUri))
                        {
                            Console.WriteLine("FIle service started");
                            //var files = ConfigurPeer.PingService.AvailableFileMetaData;

                            //if(files.Any())
                             //   Console.WriteLine($"\n Available files  {files.Count}");

                           // files.ToList().ForEach(fp =>
                          //  {
                           //     Console.WriteLine($"Filename: {fp.Name}      Size: {fp.Length}");
                           // });
                        }
                   // }));
                  //  thd.Start();
                }
            }
        }

        public void PingServiceOnPeerEndPointInformation(HostInfo endPointInfo)
        {
            Console.WriteLine("\n");
            if(endPointInfo.CallBack == null)
            {
                Console.WriteLine($"Testing {endPointInfo.Uri}");

                string uri = $"net.tcp://{endPointInfo.Uri}:{endPointInfo.Port}/FileShare";
                InstanceContext callback = new InstanceContext(new FileShareCallback());
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
                DuplexChannelFactory<IFileShareService> channel = new DuplexChannelFactory<IFileShareService>(callback, binding);
                EndpointAddress endPoint = new EndpointAddress(uri);
                IFileShareService proxy = channel.CreateChannel(endPoint);
                if(proxy != null)
                {
                    var infos = new HostInfo
                    {
                        Id = ConfigurPeer.Peer.Id,
                        Port = port,
                        Uri = RegistrPeer.PeerUri
                    };

                    proxy.PingHostService(infos, true);
                }
            }
            else
            {
                if (!currentHost.Any())
                {
                    currentHost.Add(endPointInfo.Id, endPointInfo);

                    Console.WriteLine($"Testing {endPointInfo.Uri}");

                    string uri = $"net.tcp://{endPointInfo.Uri}:{endPointInfo.Port}/FileShare";
                    InstanceContext callback = new InstanceContext(new FileShareCallback());
                    NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
                    DuplexChannelFactory<IFileShareService> channel = new DuplexChannelFactory<IFileShareService>(callback, binding);
                    EndpointAddress endPoint = new EndpointAddress(uri);
                    var proxy = channel.CreateChannel(endPoint);
                    if (proxy != null)
                    {
                        HostInfo info = new HostInfo()
                        {
                            Id = ConfigurPeer.Peer.Id,
                            Port = port,
                            Uri = RegistrPeer.PeerUri
                        };
                        proxy.PingHostService(info);
                        Console.WriteLine($"{currentHost.Count} Host currently online");
                        currentHost.ToList().ForEach(p =>
                        {
                            Console.WriteLine($"Host ID: {p.Key}\nEndPoint: {p.Value.Uri}:{p.Value.Port}");

                        });
                    }
                }
                else
                {
                    if (currentHost.Any(p => p.Key == endPointInfo.Id))
                    {
                        Console.WriteLine("Host already exist");
                    }
                    else
                    {
                        string uri = $"net.tcp://{endPointInfo.Uri}:{endPointInfo.Port}/FileShare";
                        InstanceContext callback = new InstanceContext(new FileShareCallback());
                        NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
                        DuplexChannelFactory<IFileShareService> chanal = new DuplexChannelFactory<IFileShareService>(callback, binding);
                        EndpointAddress endPoint = new EndpointAddress(uri);
                        IFileShareService proxy = chanal.CreateChannel(endPoint);
                        if (proxy != null)
                        {
                            HostInfo info = new HostInfo()
                            {
                                Id = ConfigurPeer.Peer.Id,
                                Port = port,
                                Uri = RegistrPeer.PeerUri
                            };
                            proxy.PingHostService(info);
                            Console.WriteLine($"{currentHost.Count} Host currently online");
                            currentHost.ToList().ForEach(p =>
                            {
                                Console.WriteLine($"Host ID: {p.Key}\nEndPoint: {p.Value.Uri}:{p.Value.Port}");

                            });
                        }
                    }
                }
            }
        }
    
        public bool StartFileShareService(int port, string uri)
        {
            if(uri.Any() && port > 0)
            {
                Uri[] uris = new Uri[1];
                string address = $"net.tcp://{uri}:{port}/FileShare";
                uris[0] = new Uri(address);
                IFileShareService fileShare = file;
                ServiceHost host = new ServiceHost(fileShare, uris);
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
                host.AddServiceEndpoint(typeof(IFileShareService), binding, "");
                host.Opened += HostOnOpened;
                file.CurrentHostUpDate += FileOnCurrentHostUpdate;
                try
                {
                    host.Open();
                }
                catch(Exception e)
                {
                    Console.WriteLine("||||||   " + e.Message);
                }
            }
            return isStarted;
        }

        private void FileOnCurrentHostUpdate(HostInfo info, bool isCallback)
        {
            if(isCallback)
            {
                string uri = $"net.tcp://{info.Uri}:{info.Port}/FileShare";
                InstanceContext callback = new InstanceContext(new FileShareCallback());
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
                DuplexChannelFactory< IFileShareService> channel = new DuplexChannelFactory<IFileShareService>(callback, binding);
                EndpointAddress endPoint = new EndpointAddress(uri);
                IFileShareService proxy = channel.CreateChannel(endPoint);
                if(proxy != null)
                {
                    HostInfo infos = new HostInfo
                    {
                        Id = ConfigurPeer.Peer.Id,
                        Port = port,
                        Uri = RegistrPeer.PeerUri
                    };

                    proxy.PingHostService(infos);
                    currentHost.Add(info.Id, info);
                    Console.WriteLine($"{currentHost.Count(p => p.Value.CallBack != null)} Host with direct connection");
                    Console.WriteLine($"{currentHost.Count} Host available");
                    currentHost.Distinct().ToList().ForEach(p =>
                    {
                        Console.WriteLine($"Host info: ID: {p.Key}      Host: {p.Value.Uri}:  {p.Value.Port}");
                    });
                }
            }
            else
            {
                if (info != null && currentHost.All(p => p.Key != info.Id))
                {
                    currentHost.Add(info.Id, info);
                    Console.WriteLine($"{currentHost.Count} Host currently online");
                    currentHost.ToList().ForEach(p =>
                    {
                        Console.WriteLine($"Host ID: {p.Key}\nEndPoint: {p.Value.Uri}:{p.Value.Port}");
                    });
                }
                else if (!currentHost.Any())
                {
                    if(info != null)
                        currentHost.Add(info.Id, info);

                    Console.WriteLine($"{currentHost.Count} Host currently online");
                    currentHost.ToList().ForEach(p =>
                    {
                        Console.WriteLine($"Host ID: {p.Key}'\nEndPoint: {p.Value.Uri}:{p.Value.Port}");
                    });
                }
            }
        }

        private void HostOnOpened(object sender, EventArgs e)
        {
            isStarted = true;
        }
    }
}
