namespace FileShare.Contracts.FileShareServices
{
    public interface IFileShareServiceCallback
    {
        bool isConnected(string replyMessage);
    }
}
