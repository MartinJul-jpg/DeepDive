using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.EntityFrameworkCore;

namespace DeepDive.Data
{
    public class DeepDiveContext : DbContext
    {
        public DbSet<Package>Packages { get; set; }

        public DbSet<DivingPackage>DivingPackages { get; set; }

        public DbSet<SnorkelPackage> SnorkelPackages { get; set; }

        public DbSet<Package_Product> Package_Products { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Product_Booking> Product_Bookings { get; set; }
        public DbSet<Product>Products { get; set; }
        public DbSet<RegulatorSet> RegulatorSets { get; set; }
        public DbSet<Snorkel> Snorkler { get; set; }
        public DbSet<Tank> Tanks { get; set; }
        public DbSet<BCD> BCDs { get; set; }
        public DbSet<DivingSuit> DivingSuits { get; set; }
        public DbSet<Fin> Fins { get; set; }

        public DeepDiveContext(DbContextOptions dbcontextOptions) :base(dbcontextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var products = ProductRepository.GetAllProducts();
            var packages = ProductRepository.GetAllPackages();

            /* TODO: Bookingslist is currently one booking
             * var booking = BookingRepository.GetBasket();
            */

            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Package>().HasData(packages);



            // Product table
            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("product_type")
                .HasValue<Product>("product_base")
                .HasValue<RegulatorSet>("product_RS")
                .HasValue<Snorkel>("product_SNO")
                .HasValue<Tank>("product_T")
                .HasValue<BCD>("product_BCD")
                .HasValue<DivingSuit>("product_DS")
                .HasValue<Fin>("product_F");

            modelBuilder.Entity<RegulatorSet>();
            modelBuilder.Entity<Snorkel>();
            modelBuilder.Entity<Tank>();
            modelBuilder.Entity<BCD>();
            modelBuilder.Entity<DivingSuit>();
            modelBuilder.Entity<Fin>();

            // Packages table
            modelBuilder.Entity<Package>()
            .HasDiscriminator<string>("package_base")
            .HasValue<DivingPackage>("package_Dive")
            .HasValue<SnorkelPackage>("package_SNO");

            modelBuilder.Entity<DivingPackage>();
            modelBuilder.Entity<SnorkelPackage>();


            // Booking table



            // Packages x Products linking table
            modelBuilder.Entity<Package_Product>()
                .HasKey(pk => new { pk.PackageId, pk.ProductId });


            modelBuilder.Entity<Package_Product>()
                .HasOne<Package>(p => p.Package)
                .WithMany(p => p.Package_Products)
                .HasForeignKey(fk => fk.PackageId);

            modelBuilder.Entity<Package_Product>()
                .HasOne<Product>(p => p.Product)
                .WithMany(p => p.Package_Products)
                .HasForeignKey(fk => fk.ProductId);


            // Products x Bookings linking table

            modelBuilder.Entity<Product_Booking>()
                .HasKey(pk => new { pk.ProductId, pk.BookingId });

            modelBuilder.Entity<Product_Booking>()
                .HasOne<Booking>(p => p.Booking)
                .WithMany(p => p.Product_Bookings)
                .HasForeignKey(fk => fk.BookingId);

            modelBuilder.Entity<Product_Booking>()
                .HasOne<Product>(p => p.Product)
                .WithMany(p => p.Product_Bookings)
                .HasForeignKey(fk => fk.ProductId);

            

            
            
        }
    }
}
