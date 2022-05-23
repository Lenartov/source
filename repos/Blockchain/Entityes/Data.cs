using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Blockchain
{

    [DataContract]
    public class Data : IHashable
    {
        [DataMember] public string Content { get; private set; }
        [DataMember] public string Hash { get; private set; }

        public Data() { }

        public Data(string content)
        {
            Content = content;

            Hash = GetSummaryData().GetHash();
        }

        public string GetSummaryData()
        {
            string result = "";

            result += Content;

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
