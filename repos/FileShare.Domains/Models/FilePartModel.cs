using System.Runtime.Serialization;

namespace FileShare.Domains
{
    [DataContract]
    public class FilePartModel
    {
        private File file;

        [DataMember]
        public FilePart FilePart { get; set; }

        [DataMember]
        public byte[] FileBytes { get; set; }

        [DataMember]
        public string FileId => file.Id;


        public FilePartModel(File file)
        {
            this.file = file;
        }


    }
}
