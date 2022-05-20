using System.ServiceModel;

namespace FileShare
{
    [ServiceContract(CallbackContract = typeof(IFileShareServiceCallback), SessionMode = SessionMode.Required)]
    public interface IFileShareHostService
    {
        bool Stop();
        bool Start();
    }
}
