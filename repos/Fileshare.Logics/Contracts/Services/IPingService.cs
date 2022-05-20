using System.ServiceModel;


namespace FileShare
{
    [ServiceContract(CallbackContract = typeof(IPingService))]
    public interface IPingService
    {
        [OperationContract(IsOneWay = true)]
        void Ping(HostInfo hostInfo);

        [OperationContract(IsOneWay = true)]
        void PingContent(HostInfo hostInfo, string content);

        [OperationContract(IsOneWay = true)]
        void SearchFiles(string searchTerm, string peerId);
    }
}
