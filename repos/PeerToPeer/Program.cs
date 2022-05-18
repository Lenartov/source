using Fileshare.Logics.PnrpManager;
using Fileshare.Logics.ServiceManager;
using FileShare.Contracts.Repository;
using FileShare.Contracts.Services;
using FileShare.Domains;
using System;
using System.Diagnostics;
using System.Net.PeerToPeer;
using System.Net;
using System.ServiceModel;

namespace PeerToPeer
{
    public class Program
    {
        [ServiceContract]
        public interface IP2PService
        {
            [OperationContract]
            string GetName();

            [OperationContract(IsOneWay = true)]
            void SendMessage(string message, string from);
        }

        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        public class P2PService : IP2PService
        {
            private string username;

            public P2PService(string username)
            {
                this.username = username;
            }

            public string GetName()
            {
                return username;
            }

            public void SendMessage(string message, string from)
            {
                Console.WriteLine("msg: " + message + " from: " + from);
            }
        }

        public static void Main(string[] args)
        {
            // Получение конфигурационной информации из app.config
            string port = "1900";
            string username ="username";
            string serviceUrl = null;


            //  Получение URL-адреса службы с использованием адресаIPv4 
            //  и порта из конфигурационного файла
            foreach (IPAddress address in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    serviceUrl = string.Format("net.tcp://{0}:{1}/P2PService", address, port);
                    break;
                }
            }

            // Выполнение проверки, не является ли адрес null
            if (serviceUrl == null)
            {
                Console.WriteLine(1);
            }

            // Регистрация и запуск службы WCF
            var localService = new P2PService(username);
            var host = new ServiceHost(localService, new Uri(serviceUrl));
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None;
            host.AddServiceEndpoint(typeof(IP2PService), binding, serviceUrl);
            try
            {
                host.Open();
            }
            catch (AddressAlreadyInUseException)
            {
                Console.WriteLine(2);
            }
            Console.ReadKey();
            // Создание имени равноправного участника (пира)
            var peerName = new PeerName("P2P Sample", PeerNameType.Unsecured);

            // Подготовка процесса регистрации имени равноправного участника в локальном облаке
            var peerNameRegistration = new PeerNameRegistration(peerName, int.Parse(port));
            peerNameRegistration.Cloud = Cloud.AllLinkLocal;

            // Запуск процесса регистрации
            peerNameRegistration.Start();
        }

        /*static void Main(string[] args)
        {
            
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length <= 1)
            {
                Process.Start("PeerToPeer.exe");
            }
            
            new Program().Run();
        }

        private void Run()
        {
            Peer<IPingService> peer = new Peer<IPingService>() { Username = Guid.NewGuid().ToString().Split('-')[4] };
            IPeerRegistrationRepository peerRegistration = new PeerRegistrationManager();
            IPeerNameResolverRepository peerNameResolver = new Fileshare.Logics.PnrpManager.PeerNameResolver(peer.Username);
            IPeerConfigurationService peerConfigurationService = new PeerConfigurationService(peer);

            peerRegistration.StartPeerRegistration(peer.Username, peerConfigurationService.Port);

            Console.WriteLine($"Peer Uri: {peerRegistration.PeerUrl} \n Port: {peerConfigurationService.Port}");
            Console.ReadLine();
        }*/
    }
}
