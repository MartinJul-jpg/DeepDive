using DeepDive.Models;
using Microsoft.EntityFrameworkCore;

namespace DeepDive.Data
{
    public class DeepDiveContext : DbContext
    {
        public DbSet<Package>Packages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Product>Products { get; set; }
        public DbSet<RegulatorSet> RegulatorSets { get; set; }
        public DbSet<Snorkel> Snorkler { get; set; }
        public DbSet<Tank> Tanks { get; set; }
        public DbSet<BCD> BCDs { get; set; }
        public DbSet<DivingSuit> DivingSuits { get; set; }
        public DbSet<Fin> Fins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>(p =>
            {
                p.HasMany<Product>().WithMany()
            })
        }
    }
}
