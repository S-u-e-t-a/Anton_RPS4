using System.Data.Entity;

namespace rps4
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Train> Trains { get; set; }
    }
}
