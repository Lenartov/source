using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace FileShare
{
    [DataContract]
    public class FileSerchResultModel
    {
        [DataMember]
        public string PeerId { get; set; }
        
        [DataMember]
        public ObservableCollection<FileMetaData> Files { get; set; }
    }
}
