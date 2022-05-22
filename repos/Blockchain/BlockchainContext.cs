using System.Data.Entity;

namespace Blockchain
{
    public class BlockchainContext : DbContext
    {
        public BlockchainContext() : base("DB1") { }

        public DbSet<Block> Blocks { get; set; }

    }
}
