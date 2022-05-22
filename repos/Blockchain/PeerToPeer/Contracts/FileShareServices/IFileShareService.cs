using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileShare
{
    [ServiceContract(CallbackContract = typeof(IFileShareServiceCallback), SessionMode = SessionMode.Required)]
    public interface IFileShareService
    {
        [OperationContract(IsOneWay = false)]
        FilePartModel GetAllFileByte(FileMetaData fileMeta);

        [OperationContract(IsOneWay = false)]
        FilePartModel GetFilePartBytes(FilePart filePart, FileMetaData fileMeta);

        [OperationContract(IsOneWay = false)]
        void ForwardResult(FileSerchResultModel resultModel);

        [OperationContract(IsOneWay = true)]
        void PingHostService(HostInfo info, bool isCallback = false);
    }
}
