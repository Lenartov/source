using System.Runtime.Serialization;

namespace FileShare
{
    [DataContract]
    public class FileMetaData 
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

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
