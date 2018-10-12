using ConnectApp.Data;
using Microsoft.EntityFrameworkCore;
namespace ConnectApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base (options)
        {

        }
        public DbSet<Customer> Customers { get;  set; }

    }
}
