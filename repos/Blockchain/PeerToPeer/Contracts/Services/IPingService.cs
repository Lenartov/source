using System.ServiceModel;


namespace FileShare
{
    public enum OperationType
    {
        GetBlocks,
        SendBlocks,
        SendBlock
    }

    [ServiceContract(CallbackContract = typeof(IPingService))]
    public interface IPingService
    {
        [OperationContract(IsOneWay = true)]
        void Ping(HostInfo hostInfo);

        [OperationContract(IsOneWay = true)]
        void PingContent(HostInfo sender, OperationType ot, string content = "", HostInfo reciver = null);

        [OperationContract(IsOneWay = true)]
        void SendBack(HostInfo sender, OperationType ot, string content = "", HostInfo reciver = null);

        [OperationContract(IsOneWay = true)]
        void SearchFiles(string searchTerm, string peerId);
    }
}
