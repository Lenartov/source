using FileShare.Domains;
using System.ServiceModel;


namespace FileShare.Contracts.Services
{


    [ServiceContract(CallbackContract = typeof(IPingService))]
    public interface IPingService
    {
        [OperationContract(IsOneWay = true)]
        void Ping(HostInfo hostInfo);

        [OperationContract(IsOneWay = true)]
        void SearchFiles(string searchTerm, string peerId);
    }
}
