using System;
using System.Collections.Generic;
using System.Linq;

namespace Blockchain
{
    public class Chain
    {
        public List<Block> Blocks { get; private set; }
        public Block LastBlock => Blocks.Last();

        public List<User> Users { get; private set; }
        public List<string> Datas { get; private set; }

        public List<string> Hosts { get; private set; }

        public Chain()
        {
            InitDataLists();

            LoadBlocks();

            if (Blocks.Count > 0)
            {
                if (!Check())
                    throw new System.Exception("Load error");

                return;
            }

            Block genesisBlock = new Block();
            AddBlock(genesisBlock);
        }

        private void InitDataLists()
        {
            Hosts = new List<string>();
            Hosts.Add("https://localhost:44357");

            Blocks = new List<Block>();
            Users = new List<User>();
            Datas = new List<string>();
        }

        public void AddBlock(Block block)
        {
            Blocks.Add(block);
            Save(block);
            SortDataByType(block);
            SendBlockToHosts(block);

            if (!Check())
            {
                throw new MethodAccessException("Incorrect block adding");
            }
        }

        private void SendBlockToHosts(Block block)
        {
            foreach (string host in Hosts)
            {
                BlockchainNetwork.SendBlockToHost(host, block);
            }

        }

        public Block AddHost(string ip, User user)
        {
            if (string.IsNullOrEmpty(ip))
            {
                throw new ArgumentNullException(nameof(ip), "IP null.");
            }

            Data data = new Data(ip, DataType.NODE);
            Block block = new Block(user, data, LastBlock);

            AddBlock(block);

            return block;
        }

        private void SortDataByType(Block block)
        {
            switch (block.Data.Type)
            {
                case DataType.USER:
                    Users.Add(block.User);
                    /*foreach (var host in _hosts)
                    {
                        SendBlockToHosts(host, "AddData", block.Data.Content);
                    }*/
                    break;
                case DataType.STR:
                    Datas.Add(block.Data.Content);

                    /*foreach (var host in _hosts)
                    {
                        SendBlockToHosts(host, "AddUser", $"{user.Login}&{user.Password}&{user.Role}");
                    }*/
                    break;
                case DataType.NODE:
                    Hosts.Add(block.Data.Content);
                    /*foreach (var host in _hosts)
                    {
                        SendBlockToHosts(host, "AddHost", block.Data.Content);
                    }*/
                    break;
                default:
                    throw new ArgumentException(nameof(block), "Unknown type of block");
            }
        }

        public User GetLoginedUser(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentNullException(nameof(login), "Login null.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "Password null.");
            }

            User user = Users.SingleOrDefault(b => b.Login == login);
            if (user == null)
            {
                return null;
            }

            User comparedUser = new User(login, password, UserRole.User);
            if (user.Hash != comparedUser.Hash)
            {
                return null;
            }

            return user;
        }


        public bool Check()
        {

            foreach(Block block in Blocks)
            {
                string data = block.GetSummaryData();
                string hash = data.GetHash();

                if (block.Hash != hash)
                    return false;
            }

            return true;
        }

        private void Save(Block block)
        {
            SaveToDB(block);
        }

        private void SaveToDB(Block block)
        {
            using (BlockchainContext db = new BlockchainContext())
            {
                db.Blocks.Add(block);
                db.SaveChanges();
            }
        }

        private List<Block> LoadLongestChainFromGlobal()
        {
            if (Hosts.Count < 1)
                return new List<Block>();

            List<Block> longerBlockList;
            longerBlockList = BlockchainNetwork.GetBlocksFromHost(Hosts[0]);

            foreach (string host in Hosts.Skip(1))
            {
                List<Block> blocks = BlockchainNetwork.GetBlocksFromHost(host);
                
                if(blocks.Count > longerBlockList.Count)
                {
                    longerBlockList = blocks;
                }
            }

            if (longerBlockList == null)
                return new List<Block>();

            return longerBlockList;
        }

        private List<Block> LoadFromDB()
        {
            List<Block> blocks;

            using(BlockchainContext db = new BlockchainContext())
            {
                blocks = new List<Block>(db.Blocks.Count() * 2);
                if(db.Blocks.Count() > 0)
                    blocks.AddRange(db.Blocks);
            }

            return blocks;
        }

        private void LoadBlocks()
        {
            List<Block> globalBlocks = LoadLongestChainFromGlobal();
            List<Block> localBlocks = LoadFromDB();

            if (globalBlocks.Count >= localBlocks.Count)
            {
                ClearLocalDB();

                foreach (Block block in globalBlocks)
                {
                    AddBlock(block);
                }
            }

            Blocks = localBlocks;
        }

        private void ClearLocalDB()
        {
            using (BlockchainContext db = new BlockchainContext())
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE Blocks");
                db.SaveChanges();
            }
        }
    }
}
