using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DBConnection") { }

        public DbSet<Block> Data { get; set; }

    }
}
