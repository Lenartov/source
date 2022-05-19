using System.ServiceModel;

namespace FileShare.Contracts.FileShareServices
{
    [ServiceContract(CallbackContract = typeof(IFileShareServiceCallback), SessionMode = SessionMode.Required)]
    public interface IFileShareHostService
    {
        bool Stop();
        bool Start();
    }
}
