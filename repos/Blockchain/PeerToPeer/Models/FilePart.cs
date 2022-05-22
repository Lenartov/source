using System.Runtime.Serialization;

namespace FileShare
{
    [DataContract]
    public class FilePart
    {
        [DataMember]
        public int Take { get; private set; }

        [DataMember]
        public int Skip { get; set; }

        public FilePart(int take, int skip = 0)
        {
            Take = take;
            Skip = skip;
        }
    }
}
