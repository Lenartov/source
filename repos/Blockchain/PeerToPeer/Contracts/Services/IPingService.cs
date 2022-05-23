using Blockchain;
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
        void RequestBlocks(HostInfo sender, HostInfo reciver);

        [OperationContract(IsOneWay = true)]
        void SendBlocks(HostInfo sender, HostInfo reciver, Block[] blocks);

        [OperationContract(IsOneWay = true)]
        void RequestChainInfo(HostInfo sender);

        [OperationContract(IsOneWay = true)]
        void SendChainInfo(HostInfo sender, HostInfo reciver, int chainLength, bool chainStatus);

        void SendBlock(HostInfo sender, Block block);

        [OperationContract(IsOneWay = true)]
        void SearchFiles(string searchTerm, string peerId);

       /* [OperationContract(IsOneWay = true)]
        void Ping(HostInfo hostInfo);

        [OperationContract(IsOneWay = true)]
        void RequestBlocks(HostInfo sender, OperationType ot, string content = "", HostInfo reciver = null);

        [OperationContract(IsOneWay = true)]
        void SendBack(HostInfo sender, OperationType ot, string content = "", HostInfo reciver = null);

        [OperationContract(IsOneWay = true)]
        void SearchFiles(string searchTerm, string peerId);*/
    }
}
/*
[OperationContract(IsOneWay = true)]
void Ping(HostInfo hostInfo);

[OperationContract(IsOneWay = true)]
void GetBlocks(HostInfo sender, HostInfo reciver);

[OperationContract(IsOneWay = true)]
void SendBlocks(HostInfo sender, HostInfo reciver, Block[] blocks);

[OperationContract(IsOneWay = true)]
void GetChainInfo(HostInfo sender);

[OperationContract(IsOneWay = true)]
void SendChainInfo(HostInfo sender, HostInfo reciver, int chainLength, bool chainStatus);

void SendBlock(HostInfo sender, Block block);

[OperationContract(IsOneWay = true)]
void SearchFiles(string searchTerm, string peerId);
*/