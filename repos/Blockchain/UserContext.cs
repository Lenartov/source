using System.Data.Entity;

namespace Blockchain
{
    public class UserContext : DbContext
    {
        public UserContext() : base("DBConnection") { }

        public DbSet<User> Users { get; set; }

    }
}
