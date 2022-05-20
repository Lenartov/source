using System.ServiceModel;

namespace FileShare
{
    public interface IFileShareServiceCallback
    {
        [OperationContract(IsOneWay = false)]
        bool isConnected(string replyMessage);

        [OperationContract(IsOneWay = false)]
        bool ForwardSearchResult(FileSerchResultModel serchResultModel);
    }
}
