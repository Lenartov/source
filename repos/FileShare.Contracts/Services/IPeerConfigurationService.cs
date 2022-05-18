using FileShare.Domains;

namespace FileShare.Contracts.Services
{
    public interface IPeerConfigurationService 
    { 
        int Port { get; }
        Peer<IPingService> Peer { get; }
        bool StartPeerService();
        bool StopPeerService();
    }
}
