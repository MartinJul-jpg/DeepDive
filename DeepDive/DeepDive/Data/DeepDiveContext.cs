using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
using System.Drawing;

namespace DeepDive.Data
{
    public class DeepDiveContext : IdentityDbContext<ApplicationUser>
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
            List<Product> products = new List<Product> 
            {
                new BCD
                {
                    ProductId = 10,
                    Brand = "Scubapro",
                    Model = "Navigator Lite BCD",
                    Size = "S,M,L",
                    Price = 125.0,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproNavigatorLiteBCD.jpg"
                },
                new BCD{
                    ProductId = 11,
                    Brand = "Scubapro",
                    Model = "BCD Glide",
                    Size = "S,M,L",
                    Price = 140.0,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproBCDGlide.jpg"
                },
                new BCD{
                    ProductId = 12,
                    Brand = "Scubapro",
                    Model = "BCD Hydros Pro",
                    Size = "S,M,L",
                    Price = 200.0,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproBCDHydrosPro.jpg"
                },
                new BCD{
                    ProductId = 13,
                    Brand = "Seac",
                    Model = "BCD Modular",
                    Size = "S,M,L",
                    Price = 125.0,
                    Amount = 5,
                    ImgPath = "~/images/SeacBCDModular.jpg"
                },
                new Tank{
                    ProductId = 20,
                    Brand = "ScubaPro",
                    Volume = "5 liter",
                    Price = 150.0,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproDykkerflaske5liter.jpg"
                },
                new Tank{
                    ProductId = 21,
                    Brand = "ScubaPro",
                    Volume = "10 liter",
                    Price = 160.0,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproDykkerflaske10liter.jpg"
                },
                new Tank{
                    ProductId = 22,
                    Brand = "ScubaPro",
                    Volume = "12 liter",
                    Price = 170.0,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproDykkerflaske12liter.jpg"
                },
                new Tank{
                    ProductId = 23,
                    Brand = "ScubaPro",
                    Volume = "15 liter",
                    Price = 180.0,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproDykkerflaske15liter.jpg"
                },
                new RegulatorSet{
                    ProductId = 30,
                    Brand = "ScubaPro",
                    FirstStage = "MK25EVO",
                    SecondStage = "S600",
                    Octopus = "R105",
                    Price = 125.0,
                    Amount = 5,
                    ImgPath = "~/images/scubapro_mk25_s600_regulator_2_98f86760b6.jpg"
                },
                new RegulatorSet{
                    ProductId = 31,
                    Brand = "ScubaPro",
                    FirstStage = "MK17EVO",
                    SecondStage = "C370",
                    Octopus = "R095",
                    Price = 100.0,
                    Amount = 5,
                    ImgPath = "~/images/scubapro_mk25_s600_regulator_2_98f86760b6.jpg"
                },
                new RegulatorSet{
                    ProductId = 32,
                    Brand = "ScubaPro",
                    FirstStage = "MK25EVO BT",
                    SecondStage = "A700 Carbon BT",
                    Octopus = "S270",
                    Price = 150.0,
                    Amount = 5,
                    ImgPath = "~/images/mk25-bt-a700carbon-s270.w1200.jpg"
                },
                new Fin{
                    ProductId = 40,
                    Brand = "ScubaPro",
                    Model = "Jet Fin",
                    Size = "XS,S,M,L,XL",
                    Price = 50.0,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproJetFin.jpg"
                },
                new Fin{
                    ProductId = 41,
                    Brand = "ScubaPro",
                    Model = "GO Travel",
                    Size = "XS,S,M,L,XL",
                    Price = 50.0,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproGoTravel.jpg"
                },
                new Fin{
                    ProductId = 42,
                    Brand = "ScubaPro",
                    Model = "Seawing Supernova",
                    Size = "XS,S,M,L,XL",
                    Price = 60.0,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproSeawingSupernova.jpg"
                },
                new Fin{
                    ProductId = 43,
                    Brand = "Seac",
                    Model = "Propulsion",
                    Size = "XS,S,M,L,XL",
                    Price = 50.0,
                    Amount = 5,
                    ImgPath = "~/images/SeacPropulsion.jpg"
                },
                new Fin{
                    ProductId = 44,
                    Brand = "Seac",
                    Model = "ALA",
                    Size = "XS,S,M,L,XL",
                    Price = 50.0,
                    Amount = 5,
                    ImgPath = "~/images/SeacALA.jpg"
                },
                new Fin{
                    ProductId = 45,
                    Brand = "Fourth Element",
                    Model = "Tech",
                    Size = "XS,S,M,L,XL",
                    Price = 75.0,
                    Amount = 5,
                    ImgPath = "~/images/FourthElementTech.jpg"
                },
                new Fin{
                    ProductId = 46,
                    Brand = "Fourth Element",
                    Model = "Rec Fin",
                    Size = "XS,S,M,L,XL",
                    Price = 80.0,
                    Amount = 5,
                    ImgPath = "~/images/FourthElementRecFin.jpg"
                },
                new DivingSuit
                {
                    ProductId = 50,
                    Brand = "Scubapro",
                    Model = "Definition",
                    Size = "XS,S,N,L,XL",
                    Type = "Våddragt",
                    Gender = "Herre/Dame",
                    Thickness = "3 mm",
                    Price = 100,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproDefinition3mm.jpg"
                },
                new DivingSuit
                {
                    ProductId = 51,
                    Brand = "Scubapro",
                    Model = "Definition",
                    Size = "XS,S,N,L,XL",
                    Type = "Våddragt",
                    Gender = "Herre/Dame",
                    Thickness = "5 mm",
                    Price = 100,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproDefinition5mm.jpg"
                },
                new DivingSuit
                {
                    ProductId = 52,
                    Brand = "Scubapro",
                    Model = "Definition",
                    Size = "XS,S,N,L,XL",
                    Type = "Våddragt",
                    Gender = "Herre/Dame",
                    Thickness = "7 mm",
                    Price = 100,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproDefinition7mm.jpg"
                },
                new DivingSuit
                {
                    ProductId = 53,
                    Brand = "Waterproof",
                    Model = "W5",
                    Size = "XS,S,N,L,XL",
                    Type = "Våddragt",
                    Gender = "Herre/Dame",
                    Thickness = "3.5 mm",
                    Price = 100,
                    Amount = 5,
                    ImgPath = "~/images/WaterproofW5.jpg"
                },
                new DivingSuit
                {
                    ProductId = 54,
                    Brand = "Fourth Element",
                    Model = "Proteus",
                    Size = "XS,S,N,L,XL",
                    Type = "Våddragt",
                    Gender = "Herre/Dame",
                    Thickness = "5 mm",
                    Price = 120,
                    Amount = 5,
                    ImgPath = "~/images/FourthElementProteus.jpg"
                },
                new DivingSuit
                {
                    ProductId = 55,
                    Brand = "Scubapro",
                    Model = "Exodry 4.0",
                    Size = "XS,S,N,L,XL",
                    Type = "Tørdragt",
                    Gender = "Herre/Dame",
                    Thickness = " ",
                    Price = 300,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproExodry4-0.jpg"
                },
                new DivingSuit
                {
                    ProductId = 56,
                    Brand = "Waterproof",
                    Model = "D7 Evo",
                    Size = "XS,S,N,L,XL",
                    Type = "Tørdragt",
                    Gender = "Herre/Dame",
                    Thickness = " ",
                    Price = 320,
                    Amount = 5,
                    ImgPath = "~/images/WaterproofD7Evo.jpg"
                },
                new DivingSuit
                {
                    ProductId = 57,
                    Brand = "Santi",
                    Model = "E.Lite plus",
                    Size = "XS,S,N,L,XL",
                    Type = "Tørdragt",
                    Gender = "Herre/Dame",
                    Thickness = " ",
                    Price = 350,
                    Amount = 5,
                    ImgPath = "~/images/SantiE-LitePlus.jpg"
                },
                new Snorkel{
                    ProductId = 60,
                    Brand = "Scubapro",
                    Model = "Ghost",
                    Price = 50,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproGhost.jpg"
                },
                new Snorkel{
                    ProductId = 61,
                    Brand = "Scubapro",
                    Model = "D-Mask",
                    Price = 60,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproD-Mask.jpg"
                },
                new Snorkel{
                    ProductId = 62,
                    Brand = "Scubapro",
                    Model = "Spectra Mini",
                    Price = 50,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproSpectraMini.jpg"
                },
                new Snorkel{
                    ProductId = 63,
                    Brand = "Scubapro",
                    Model = "Crystal VU",
                    Price = 75,
                    Amount = 5,
                    ImgPath = "~/images/ScubaproCrystalVU.jpg"
                },
                new Snorkel{
                    ProductId = 64,
                    Brand = "Fourth Element",
                    Model = "Scout Kontrast",
                    Price = 75,
                    Amount = 5,
                    ImgPath = "~/images/FourthElementScoutKontrast.jpg"
                },
                new Snorkel{
                    ProductId = 65,
                    Brand = "Fourth Element",
                    Model = "Scout Enhance",
                    Price = 75.0,
                    Amount = 5,
                    ImgPath = "~/images/FourthElementScoutEnhance.jpg"
                },
                new Snorkel{
                    ProductId = 66,
                    Brand = "Tusa",
                    Model = "Element",
                    Price = 75.0,
                    Amount = 5,
                    ImgPath = "~/images/TusaElement.jpg"
                },
            };

            List<(Package, List<int>)> packages = new List<(Package, List<int>)>()
            {
                (
                    new Package()
                    {
                        PackageId = 10
                    }, 
                    new List<int>() { 10, 50, 30, 20, 40, 60 }
                ),
                (
                    new Package()
                    {
                        PackageId = 20
                    },
                    new List<int>() { 60, 40 }
                )
            };

            List<(Booking, List<int>)> bookings = new List<(Booking, List<int>)>()
            {
                (
                    new Booking()
                    {
                        BookingId = 1,
                        Start = DateOnly.FromDateTime(DateTime.Now),
                        End = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                        UserId = "84cd148c-e055-4d01-8a32-d06e7489e2a3"
                    },
                    new List<int>() { 10, 10, 10 }
                ),
                (
                    new Booking()
                    {
                        BookingId = 2,
                        Start = DateOnly.FromDateTime(DateTime.Now),
                        End = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                        UserId = "84cd148c-e055-4d01-8a32-d06e7489e2a3"
                    },
                    new List<int> { 20, 20, 20 }
                )
            };

            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "1ac343b0-10ef-1a1c-1f64-18daaca77b5b"
                },
                new IdentityRole()
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "2ac343b0-20ef-2a1c-2f64-28daaca77b5b"
                }
            };

            List<IdentityUser> users = new List<IdentityUser>()
            {
                new ApplicationUser()
                {
                    Id = "84cd148c-e055-4d01-8a32-d06e7489e2a3",
                    UserName = "kaede@mail.weeb",
                    NormalizedUserName = "KAEDE@MAIL.WEEB",
                    Email = "kaede@mail.weeb",
                    NormalizedEmail = "KAEDE@MAIL.WEEB",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEPLm8PWZaT7SCiLonPE0fCuRfv1/I5tpvitirqwxafypHXTXHtZXEe2fwvsW26UyPQ==",
                    SecurityStamp = "E6PKSVRQKLTX6JM23QOOG7NSEMZAT47O",
                    ConcurrencyStamp = "451190ea-b605-4320-980a-6b863f169b73",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new ApplicationUser()
                {
                    Id = "9542f2d5-1f05-405c-b9df-eead849cd99f",
                    UserName = "admin@mail.weeb",
                    NormalizedUserName = "ADMIN@MAIL.WEEB",
                    Email = "admin@mail.weeb",
                    NormalizedEmail = "ADMIN@MAIL.WEEB",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEBEVmLtgwFMZs35MUjXhKmcugpijRN4PRdczdzESGAEHJ3JA9IPKoq6XCexeD3KMtg==",
                    SecurityStamp = "OINHVGDBRL3BYMS5GX6ZUR6LIH6YPQNN",
                    ConcurrencyStamp = "e322824d-2dab-48bc-ade6-9fc4d2a873bc",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            };

            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    RoleId = "1",
                    UserId = "9542f2d5-1f05-405c-b9df-eead849cd99f"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2",
                    UserId = "9542f2d5-1f05-405c-b9df-eead849cd99f"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2",
                    UserId = "84cd148c-e055-4d01-8a32-d06e7489e2a3"
                }
            };

            //pre-seed users
            modelBuilder.Entity<ApplicationUser>().HasData(users);

            // Pre-seed roles
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            //pre-seed userRoles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);

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
            foreach ((Package, List<int>) tuple in packages)
            {
                modelBuilder.Entity<Package>().HasData(tuple.Item1);

                List<Package_Product> package_products = new List<Package_Product>();
                foreach (int productId in tuple.Item2)
                {
                    package_products.Add(new Package_Product() { PackageId = tuple.Item1.PackageId, ProductId = productId });
                }
                modelBuilder.Entity<Package_Product>().HasData(package_products);
            }

            // pre-seed Bookings
            int i = 0;
            foreach ((Booking, List<int>) tuple in bookings)
            {
                modelBuilder.Entity<Booking>().HasData(tuple.Item1);

                List<Product_Booking> product_bookings = new List<Product_Booking>();
                foreach (int productId in tuple.Item2)
                {
                    i += 1;
                    product_bookings.Add(new Product_Booking() { Product_BookingId = i, BookingId = tuple.Item1.BookingId, ProductId = productId });
                }
                modelBuilder.Entity<Product_Booking>().HasData(product_bookings);
            }

            // Product table
            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("product_type")
                .HasValue<Product>("product_base")
                .HasValue<RegulatorSet>("product_RegulatorSet")
                .HasValue<Snorkel>("product_Snorkel")
                .HasValue<Tank>("product_Tank")
                .HasValue<BCD>("product_BCD")
                .HasValue<DivingSuit>("product_DivingSuit")
                .HasValue<Fin>("product_Fin");
            
            modelBuilder.Entity<Snorkel>();
            modelBuilder.Entity<Tank>();
            modelBuilder.Entity<BCD>();
            modelBuilder.Entity<DivingSuit>();
            modelBuilder.Entity<Fin>();
            modelBuilder.Entity<RegulatorSet>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .HasOne<ApplicationUser>(b => b.User)
                .WithMany(a => a.Bookings)
                .HasForeignKey(b => b.UserId);

            // Booking table
            modelBuilder.Entity<Booking>()
                .Ignore(booking => booking.DurationInDays);
            modelBuilder.Entity<Booking>()
                .Ignore(booking => booking.TotalPrice);
            modelBuilder.Entity<Booking>()
                .Ignore(booking => booking.ExceedsStock);

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
                .HasKey(pk => new { pk.Product_BookingId });

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
