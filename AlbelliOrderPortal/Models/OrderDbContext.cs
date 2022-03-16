using AlbelliOrderPortal.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AlbelliOrderPortal.Models
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Extension method called to prevent pluralzing table names
            modelBuilder.RemovePluralizingTableNameConvention();
            modelBuilder.Entity<Order>().HasKey(k => k.OrderId);
            modelBuilder.Entity<OrderLine>().HasKey(k => k.Id);
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderLine> OrderLine { get; set; }
    }
}
