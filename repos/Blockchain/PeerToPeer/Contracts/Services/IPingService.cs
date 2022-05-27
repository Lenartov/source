using Blockchain;
using System.ServiceModel;


namespace FileShare
{
    [ServiceContract(CallbackContract = typeof(IPingService))]
    public interface IPingService
    {
        [OperationContract(IsOneWay = true)]
        void Ping(HostInfo hostInfo);

        [OperationContract(IsOneWay = true)]
        void RequestBlocks(HostInfo sender, HostInfo reciver);

        [OperationContract(IsOneWay = true)]
        void SendBlocks(HostInfo sender, HostInfo reciver, string blocksJson);

        [OperationContract(IsOneWay = true)]
        void RequestChainInfo(HostInfo sender);

        [OperationContract(IsOneWay = true)]
        void SendChainInfo(HostInfo sender, HostInfo reciver, int chainLength, bool chainStatus);

        [OperationContract(IsOneWay = true)]
        void SendBlock(HostInfo sender, Block block);

        [OperationContract(IsOneWay = true)]
        void SearchFiles(string searchTerm, string peerId);
    }
}
