using System;
using System.Collections.Generic;
using System.Linq;
using FileShare;

namespace Blockchain
{
    public class Chain
    {
        public Action OnBlocksListChange;
        public bool sendreg = false;
        public string json;
        public HostInfo reciver;

        private List<Block> blocks = new List<Block>();

        public List<Block> Blocks
        {
            get
            {
                return blocks;
            }
            set
            {
                blocks = value;
            }
        }
        public Block LastBlock => Blocks.Last();
        public IPingService pingService;

        public List<User> Users { get; private set; } = new List<User>();
        public List<string> Datas { get; private set; } = new List<string>();

        public static Chain Instance;

        public Chain(PeerServiceHost peerService)
        {
            pingService = peerService.ConfigurPeer.Peer.Channel;

            if (Instance == null)
                Instance = this;

            InitDataLists();

            Blocks = LoadFromDB();

            RequestChainInfo();

            if (Blocks.Count < 1)
            {
                AddBlock(new Block());
            }

            SortBlocksByType();

            // pingService.PingContent(CurrentHost.Instance.Info, OperationType.GetBlocks);

        }

        public bool CompareBlocks(List<Block> blocks)
        {
            if(Blocks.Count < blocks.Count)
            {
                if (Check(blocks))
                    return true;
            }
            return false;
        }

        public void RequestChainInfo()
        {
            pingService.RequestChainInfo(CurrentHost.Instance.Info);
        }

        public void RequestBlocks(HostInfo reciver)
        {
            pingService.RequestBlocks(CurrentHost.Instance.Info, reciver);
        }


        public void SendBlocks(HostInfo reciver)
        {
            pingService.SendBlocks(CurrentHost.Instance.Info, reciver, Blocks.ToArray());
        }

        public void SendChainInfo(HostInfo reciver)
        {
            pingService.SendChainInfo(CurrentHost.Instance.Info, reciver, Blocks.Count, Check(Blocks));
        }

        public void Ping()
        {
            //pingService.Ping(CurrentHost.Instance.Info);

          /*  if(sendreg)
            {
                sendreg = false;
                pingService.SendBack(CurrentHost.Instance.Info, OperationType.GetBlocks, json, reciver);
            }
            else
                pingService.RequestBlocks(CurrentHost.Instance.Info, OperationType.GetBlocks);*/
        }

        private void InitDataLists()
        {
            Blocks = new List<Block>();
            Users = new List<User>();
            Datas = new List<string>();


            OnBlocksListChange?.Invoke();
        }

        public void AddBlock(Block block)
        {
            SendBlockToHost(block);

            Blocks.Add(block);
            Save(block);
            SortBlockByType(block);

            if (!Check(Blocks))
            {
                throw new MethodAccessException("Incorrect block adding");
            }

            OnBlocksListChange?.Invoke();
        }

        public void AddUser(User user)
        {
            Data data = new Data(user.GetJson());

            Block block = new Block(user, data, LastBlock, BlockType.USER);
            AddBlock(block);
        }

        private void SendBlockToHost(Block block)
        {

        }

        private void SortBlocksByType()
        {
            Users = new List<User>();
            Datas = new List<string>();

            foreach (Block block in Blocks)
            {
                SortBlockByType(block);
            }
        }

        private void SortBlockByType(Block block)
        {
            switch (block.BlockType)
            {
                case BlockType.USER:
                    {
                        Users.Add(block.User);
                        break;
                    }
                case BlockType.STR:
                    {
                        Datas.Add(block.Data.Content);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException(nameof(block), "Unknown type of block");
                    }
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


        public bool Check(List<Block> blocks)
        {

            foreach(Block block in blocks)
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

        public void SetBlocksFromGlobal(List<Block> blocks)
        {
            Blocks = blocks;
            OnBlocksListChange?.Invoke();

            SyncDBWithGlobal();

            SortBlocksByType();

        }

        private void SyncDBWithGlobal()
        {
            ClearLocalDB();

            using (BlockchainContext db = new BlockchainContext())
            {
                db.Blocks.AddRange(Blocks);
                db.SaveChanges();
            }
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
