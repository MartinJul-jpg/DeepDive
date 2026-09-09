using DeepDive.Models;

namespace DeepDive.Repository
{
    public static class ProductRepository
    {
        private static List<Product> products = new List<Product> {
            new BCD
            {
                ProductId = 10,
                Brand = "Scubapro",
                Model = "Navigator Lite BCD",
                Size = "S,M,L",
                Price = 125.0,
                Amount = 1
            },
            new BCD{
                ProductId = 11,
                Brand = "Scubapro",
                Model = "BCD Glide",
                Size = "S,M,L",
                Price = 140.0,
                Amount = 1
            },
            new BCD{
                ProductId = 12,
                Brand = "Scubapro",
                Model = "BCD Hydros Pro",
                Size = "S,M,L",
                Price = 200.0,
                Amount = 1
            },
            new BCD{
                ProductId = 13,
                Brand = "Seac",
                Model = "BCD Modular",
                Size = "S,M,L",
                Price = 125.0,
                Amount = 1
            },
            new Tank{
                ProductId = 20,
                Brand = "ScubaPro",
                Volume = "5 liter",
                Price = 150.0,
                Amount = 1
            },
            new Tank{
                ProductId = 21,
                Brand = "ScubaPro",
                Volume = "10 liter",
                Price = 160.0,
                Amount = 1
            },
            new Tank{
                ProductId = 22,
                Brand = "ScubaPro",
                Volume = "12 liter",
                Price = 170.0,
                Amount = 1
            },
            new Tank{
                ProductId = 23,
                Brand = "ScubaPro",
                Volume = "15 liter",
                Price = 180.0,
                Amount = 1
            },
            new RegulatorSet{
                ProductId = 30,
                Brand = "ScubaPro",
                FirstStage = "MK25EVO",
                SecondStage = "S600",
                Octopus = "R105",
                Price = 125.0,
                Amount = 1
            },
            new RegulatorSet{
                ProductId = 31,
                Brand = "ScubaPro",
                FirstStage = "MK17EVO",
                SecondStage = "C370",
                Octopus = "R095",
                Price = 100.0,
                Amount = 1
            },
            new RegulatorSet{
                ProductId = 32,
                Brand = "ScubaPro",
                FirstStage = "MK25EVO BT",
                SecondStage = "A700 Carbon BT",
                Octopus = "S270",
                Price = 150.0,
                Amount = 1
            },
            new Fin{
                ProductId = 40,
                Brand = "ScubaPro",
                Model = "Jet Fin",
                Size = "XS,S,M,L,XL",
                Price = 50.0,
                Amount = 1
            },
            new Fin{
                ProductId = 41,
                Brand = "ScubaPro",
                Model = "GO Travel",
                Size = "XS,S,M,L,XL",
                Price = 50.0,
                Amount = 1
            },
            new Fin{
                ProductId = 42,
                Brand = "ScubaPro",
                Model = "Seawing Supernova",
                Size = "XS,S,M,L,XL",
                Price = 60.0,
                Amount = 1
            },
            new Fin{
                ProductId = 43,
                Brand = "Seac",
                Model = "Propulsion",
                Size = "XS,S,M,L,XL",
                Price = 50.0,
                Amount = 1
            },
            new Fin{
                ProductId = 44,
                Brand = "Seac",
                Model = "ALA",
                Size = "XS,S,M,L,XL",
                Price = 50.0,
                Amount = 1
            },
            new Fin{
                ProductId = 45,
                Brand = "Fourth Element",
                Model = "Tech",
                Size = "XS,S,M,L,XL",
                Price = 75.0,
                Amount = 1
            },
            new Fin{
                ProductId = 46,
                Brand = "Fourth Element",
                Model = "Rec Fin",
                Size = "XS,S,M,L,XL",
                Price = 80.0,
                Amount = 1
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
                Amount = 1
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
                Amount = 1
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
                Amount = 1
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
                Amount = 1
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
                Amount = 1
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
                Amount = 1
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
                Amount = 1
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
                Amount = 1
            },
            new Snorkel{
                ProductId = 60,
                Brand = "Scubapro",
                Model = "Ghost",
                Price = 50,
                Amount = 1
            },
            new Snorkel{
                ProductId = 61,
                Brand = "Scubapro",
                Model = "D-Mask",
                Price = 60,
                Amount = 1
            },
            new Snorkel{
                ProductId = 62,
                Brand = "Scubapro",
                Model = "Spectra Mini",
                Price = 50,
                Amount = 1
            },
            new Snorkel{
                ProductId = 63,
                Brand = "Scubapro",
                Model = "Crystal VU",
                Price = 75,
                Amount = 1
            },
            new Snorkel{
                ProductId = 64,
                Brand = "Fourth Element",
                Model = "Scout Kontrast",
                Price = 75,
                Amount = 1
            },
            new Snorkel{
                ProductId = 65,
                Brand = "Fourth Element",
                Model = "Scout Enhance",
                Price = 75.0,
                Amount = 1
            },
            new Snorkel{
                ProductId = 66,
                Brand = "Tusa",
                Model = "Element",
                Price = 75.0,
                Amount = 1
            },
        };
        private static List<Package> packages = new List<Package>()
        {
            new DivingPackage(GetProductById(10) as BCD, GetProductById(50) as DivingSuit, GetProductById(30) as RegulatorSet, GetProductById(20) as Tank, GetProductById(40) as Fin, GetProductById(60) as Snorkel)
            {
                PackageId = 10
            }, 
            new SnorkelPackage(GetProductById(60) as Snorkel, GetProductById(40) as Fin)
            {
                PackageId = 20
            }
        };

        public static List<Product> GetAllProducts()
        {
            return products;
        }

        public static Product? RemoveAmount (int id, int amount)
        {
            Product Tmproduct = GetProductById(id)!;
            Tmproduct.Amount -= amount;
            return Tmproduct;
        }

        public static Product? GetProductById(int id)
        {
            return products.FirstOrDefault(product => product.ProductId == id);
        }

        public static List<Package> GetAllPackages()
        {
            return packages;
        }

        public static Package? GetPackageById(int id)
        {
            return packages.FirstOrDefault(package => package.PackageId == id);
        }
    }
}