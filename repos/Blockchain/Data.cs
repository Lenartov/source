using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Blockchain
{
    public enum DataType
    {
        STR,
        USER,
        NODE
    }

    [DataContract]
    public class Data : IHashable
    {
        [DataMember] public string Content { get; private set; }
        [DataMember] public string Hash { get; private set; }
        [DataMember] public DataType Type { get; private set; }

        public Data() { }

        public Data(string content, DataType type)
        {
            Content = content;
            Type = type;

            Hash = GetSummaryData().GetHash();
        }

        public string GetSummaryData()
        {
            string result = "";

            result += Content;
            result += Type;

            return result;
        }

        public string GetJson()
        {
            var jsonFormatter = new DataContractJsonSerializer(GetType());

            using (var ms = new MemoryStream())
            {
                jsonFormatter.WriteObject(ms, this);
                var jsonString = Encoding.UTF8.GetString((ms.ToArray()));
                return jsonString;
            }
        }
    }
}
