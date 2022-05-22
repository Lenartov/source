using System;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Blockchain
{
    [DataContract]
    public class Block : IHashable
    {
        public int Id { get; private set; }
        [DataMember] public Data Data { get; private set; }
        [DataMember] public DateTime Created { get; private set; }
        [DataMember] public string Hash { get; private set; }
        [DataMember] public string PrevHash { get; private set; }
        [IgnoreDataMember] public User User { get; private set; }

        public Block()
        {
            Id = 0;
            Created = DateTime.Parse("11.05.2022 00:00:00.000");
            User = new User("Penis", "12345678987654321", UserRole.Admin);
            Data = new Data(User.GetJson(), DataType.USER);

            PrevHash = "3mb90y-45bhjmj43m-t-03,j43gfvk,-".GetHash();
            Hash = GetSummaryData().GetHash(); // GenerateHash(summaryData);
        }

        public Block(User user, Data data, Block prevBlock)
        {
            if (user == null)
            {
                throw new ArgumentNullException($"Empty user", nameof(user));
            }

            if (string.IsNullOrWhiteSpace(data.Content))
            {
                throw new ArgumentNullException($"Empty data", nameof(data));
            }


            if (prevBlock is null)
            {
                throw new ArgumentNullException($"Empty block", nameof(prevBlock));
            }

            User = user;
            Data = data;
            Created = DateTime.UtcNow.ToUniversalTime();
            PrevHash = prevBlock.Hash;
            Id = prevBlock.Id + 1;

            Hash = GetSummaryData().GetHash();
        }

        public string GetSummaryData()
        {
            string result = "";

            result += Created;/*.ToString("dd.MM.yyyy HH:mm:ss.fff");*/
            result += PrevHash;

            result += Data.GetSummaryData().GetHash();
            result += User.GetSummaryData().GetHash();

            return result;
        }

        private string GenerateHash(string data)
        {
            byte[] dataBytes = Encoding.ASCII.GetBytes(data);
            SHA256Managed hashManager = new SHA256Managed();
            string hashHex = "";

            var hashValue = hashManager.ComputeHash(dataBytes);

            foreach(byte i in hashValue)
            {
                hashHex += string.Format("{0:x2}", i);
            }

            return hashHex;
        }

        public string Serialize()
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Block));
            
            using(MemoryStream ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);

                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public static Block Deserialize(string json)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Block));

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {

                return (Block)jsonSerializer.ReadObject(ms);
            }
        }

        public override string ToString()
        {
            return Data.Content;
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
