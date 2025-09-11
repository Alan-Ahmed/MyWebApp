using Microsoft.EntityFrameworkCore;
using MyWebApp.DomainLayer.Entities;

namespace MyWebApp.InfrastructureLayer.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet för varje entitet
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
    }
}
