using System;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using Blockchain.Extensions;

namespace Blockchain
{
    public enum BlockType
    {
        STR,
        USER,
        FILE
    }

    [DataContract]
    public class Block : IHashable
    {
        public int Id { get; private set; }
        [DataMember] public Data Data { get; private set; }
        [DataMember] public DateTime Created { get; private set; }
        [DataMember] public string Hash { get; private set; }
        [DataMember] public string PrevHash { get; private set; }
        [DataMember] public User User { get; private set; }
        [DataMember] public BlockType BlockType { get; private set; }

        public Block()
        {
            Id = 0;
            Created = DateTime.Parse("11.05.2022 00:00:00.000");
            User = new User("Penis", "12345678987654321", UserRole.Admin);
            Data = new Data("Genesis user admin block");
            BlockType = BlockType.USER;

            PrevHash = "3mb90y-45bhjmj43m-t-03,j43gfvk,-".GetHash();
            Hash = GetSummaryData().GetHash();
        }

        public Block(User user, Data data, Block prevBlock, BlockType blockType)
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
            BlockType = blockType;

            Hash = GetSummaryData().GetHash();
        }

        public string GetSummaryData()
        {
            string result = "";

            result += Created;
            result += PrevHash;

            result += Data.GetSummaryData().GetHash();
            result += User.GetSummaryData().GetHash();

            return result;
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

        public void ViewContent()
        {
            switch(BlockType)
            {
                case BlockType.USER:
                {
                        MessageBox.Show("This block contain user info"); 
                    break;
                }
                case BlockType.STR:
                {
                        MessageBox.Show("This block contain message: \n" + Data.Content);

                        break;
                }
                case BlockType.FILE:
                    {
                        string downloadDir = SystemPaths.GetDownloadFolderPath();
                        string path = downloadDir + "\\TempFile" + Data.FileType;

                        try
                        {
                            Data.Content.TryCreateFileFromBinary(path);

                            ProcessStartInfo psi = new ProcessStartInfo();

                            psi.FileName = path;
                            Process p = new Process();
                            p.StartInfo = psi;
                            p.Start();
                           // p.EnableRaisingEvents = true;
                           // p.Exited += P_Exited;
                        }
                        catch
                        {
                            MessageBox.Show("File open error");
                        }
                        break;
                    }
            }
        }

        private void P_Exited(object sender, EventArgs e)
        {
            string downloadDir = SystemPaths.GetDownloadFolderPath();
            string path = downloadDir + "\\TempFile" + Data.FileType;

            try
            {
                path.TryDeleteFile();
            }
            catch
            {
                MessageBox.Show("TempFile are not deleted\nPlease delete it manual in Downloads folder");
            }
        }

        public void CopyHashToBuffer()
        {
            Clipboard.SetText(Hash);
        }

        public void CopyLoginToBuffer()
        {
            Clipboard.SetText(User.Login);
        }

        public void DownLoadContentTo(string path)
        {
            Data.Content.TryCreateFileFromBinary(path + Data.FileType);
        }
    }
}
