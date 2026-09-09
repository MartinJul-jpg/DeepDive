using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.EntityFrameworkCore;

namespace DeepDive.Data
{
    public class DeepDiveContext : DbContext
    {
        public DbSet<Package>Packages { get; set; }
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

        public DeepDiveContext(DbContextOptions dbcontextOptions) :base(dbcontextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var products = ProductRepository.GetAllProducts();
            var packages = ProductRepository.GetAllPackages();

            //pre-seed different product-type tables
            foreach (var product in products)
            {
                if (product is RegulatorSet)
                {
                    modelBuilder.Entity<RegulatorSet>().HasData(product);
                }
                if (product is Snorkel)
                {
                    modelBuilder.Entity<Snorkel>().HasData(product);
                }
                if (product is Tank)
                {
                    modelBuilder.Entity<Tank>().HasData(product);
                }
                if (product is BCD)
                {
                    modelBuilder.Entity<BCD>().HasData(product);
                }
                if (product is DivingSuit)
                {
                    modelBuilder.Entity<DivingSuit>().HasData(product);
                }
                if (product is Fin)
                {
                    modelBuilder.Entity<Fin>().HasData(product);
                }
            }

            // pre-seed packages
            foreach (Package package in packages)
            {
                modelBuilder.Entity<Package>().HasData(package);

                List<Package_Product> package_products = new List<Package_Product>();
                foreach (Product product in package.SetList)
                {
                    package_products.Add(new Package_Product() { PackageId = package.PackageId, ProductId = product.ProductId });
                }

                modelBuilder.Entity<Package_Product>().HasData(package_products);
            }

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
            
            modelBuilder.Entity<Snorkel>();
            modelBuilder.Entity<Tank>();
            modelBuilder.Entity<BCD>();
            modelBuilder.Entity<DivingSuit>();
            modelBuilder.Entity<Fin>();
            modelBuilder.Entity<RegulatorSet>();

            // Package table
            modelBuilder.Entity<Package>()
                .Ignore(package => package.SetList);

            // Booking table
            modelBuilder.Entity<Booking>()
                .Ignore(booking => booking.BookedProducts);

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
