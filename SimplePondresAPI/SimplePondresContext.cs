using Microsoft.EntityFrameworkCore;
using SimplePondresAPI.Models;

namespace SimplePondresAPI
{
    public class SimplePondresContext : DbContext
    {
        public SimplePondresContext(DbContextOptions<SimplePondresContext> options):base(options)
        {

        }

        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
    }
}
