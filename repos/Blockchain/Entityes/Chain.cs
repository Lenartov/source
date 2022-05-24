using System;
using System.Collections.Generic;
using System.Linq;
using FileShare;

namespace Blockchain
{
    public class Chain
    {
        private object locker = new object();
        public Action OnBlocksListChange;

        private SynchronizedCollection<Block> blocks = new SynchronizedCollection<Block>();

        public SynchronizedCollection<Block> Blocks
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
        public Block LastBlock 
        { 
            get
            {
                if (Blocks.Count == 0)
                    AddBlock(new Block());

                return Blocks.Last();
            } 
        }
        public IPingService pingService;

        public List<User> Users { get; private set; } = new List<User>();
        public List<string> Datas { get; private set; } = new List<string>();

        public static Chain Instance;

        public Chain(PeerServiceHost peerService)
        {
            if (Instance == null)
                Instance = this;

            InitDataLists();

            pingService = peerService.ConfigurPeer.Peer.Channel;

            Blocks = LoadFromDB();

            RequestChainInfo();

            SortBlocksByType();

            if (Blocks.Count < 1)
            {
                AddBlock(new Block());
            }
        }

        public bool CompareBlocks(SynchronizedCollection<Block> blocks)
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

        private void InitDataLists()
        {
            Blocks = new SynchronizedCollection<Block>();
            Users = new List<User>();
            Datas = new List<string>();

            OnBlocksListChange?.Invoke();
        }

        public void AddBlockLocal(Block block)
        {
            SaveToDB(block);
            SortBlockByType(block);

            OnBlocksListChange?.Invoke();
        }

        public void AddBlock(Block block)
        {
            Blocks.Add(block);

            if (!Check(Blocks))
            {
                Blocks.Remove(LastBlock);
                return;
            }

            AddBlockLocal(block);
            SendBlockToHost(block);
        }

        public void AddUser(User user)
        {
            Data data = new Data(user.GetJson());

            Block block = new Block(user, data, LastBlock, BlockType.USER);
            if (!Blocks.Contains(block))
                AddBlock(block);
        }

        private void SendBlockToHost(Block block)
        {
            pingService.SendBlock(CurrentHost.Instance.Info, block);
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


        public bool Check(SynchronizedCollection<Block> blocks)
        {
            if (blocks.Count < 1)
                return false;

            string data = blocks[0].GetSummaryData();
            string hash = data.GetHash();

            if (blocks[0].Hash != hash)
                return false;

            for (int i = 1; i < blocks.Count; i++)
            {
                if (blocks[i].PrevHash != blocks[i - 1].Hash)
                    return false;

                data = blocks[i].GetSummaryData();
                hash = data.GetHash();

                if (blocks[i].Hash != hash)
                    return false;
            }

            return true;
        }

        private void SaveToDB(Block block)
        {
            lock (locker)
            {
                using (BlockchainContext db = new BlockchainContext())
                {
                    Block[] arr = db.Blocks.ToArray();

                    foreach (var a in arr)
                    {
                        if (a.Hash == block.Hash)
                            return;
                    }

                    db.Blocks.Add(block);
                    db.SaveChanges();
                }
            }
        }

        private SynchronizedCollection<Block> LoadFromDB()
        {
            SynchronizedCollection<Block> blocks;

            using (BlockchainContext db = new BlockchainContext())
            {
                SynchronizedCollection<Block> syncBlocks = new SynchronizedCollection<Block>();

                foreach (var s in db.Blocks)
                    syncBlocks.Add(s);

                if (!Check(syncBlocks))
                {
                    ClearLocalDB();
                    return new SynchronizedCollection<Block>();
                }

                blocks = new SynchronizedCollection<Block>(db.Blocks.Count() * 2);

                if (db.Blocks.Count() > 0)
                {
                    foreach(var s in syncBlocks)
                        blocks.Add(s);
                }
            }



            return blocks;
        }

        public void SetBlocksFromGlobal(SynchronizedCollection<Block> blocks)
        {
            Blocks = blocks;
            OnBlocksListChange?.Invoke();

            SyncDBWithGlobal();

            SortBlocksByType();

        }

        private void SyncDBWithGlobal()
        {
            lock (locker)
            {
                ClearLocalDB();

                using (BlockchainContext db = new BlockchainContext())
                {
                    List<Block> hyi = db.Blocks.ToList();

                    foreach (var h in hyi)
                        db.Blocks.Remove(h);

                    db.Blocks.AddRange(Blocks);
                    db.SaveChanges();
                }
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
