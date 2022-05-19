using System.Runtime.Serialization;

namespace FileShare.Domains
{
    [DataContract]
    public class FileMetaData 
    {
        [DataMember]
        public string Id { get; }

        [DataMember]
        public string Name { get; }

        [DataMember]
        public int Length { get; }

        public FileMetaData(string id, string name, int length)
        {
            Id = id;
            Name = name;
            Length = length;
        }
    }
}
