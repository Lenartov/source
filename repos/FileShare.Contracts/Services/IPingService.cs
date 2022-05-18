using System.ServiceModel;


namespace FileShare.Contracts.Services
{
    [ServiceContract(CallbackContract = typeof(IPingService))]
    public interface IPingService
    {
        [OperationContract(IsOneWay = true)]
        void Ping(int port, string peerUri);
    }
}
