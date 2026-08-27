using DeepDive.Models;

namespace DeepDive.Repository
{
    public class ProductRepository
    {
        private static List<Product> products = new List<Product> {
        new BCD
        {
            ProductId = 1,
            Brand = "Scubapro",
            Model = "Navigator Lite BCD",
            Size = "S,M,L",
            Price = 125.0
        },
        new BCD{
            ProductId = 2,
            Brand = "Scubapro",
            Model = "BCD Glide",
            Size = "S,M,L",
            Price = 140.0
        },

        new BCD{
            ProductId = 3,
            Brand = "Scubapro",
            Model = "BCD Hydros Pro",
            Size = "S,M,L",
            Price = 200.0
        },
        new BCD{
            ProductId = 4,
            Brand = "Seac",
            Model = "BCD Modular",
            Size = "S,M,L",
            Price = 125.0
        },
        new Tank{
            ProductId = 5,
            Brand = "ScubaPro",
            Volume = "5 liter",
            Price = 150.0
        },
        new Tank{
            ProductId = 6,
            Brand = "ScubaPro",
            Volume = "10 liter",
            Price = 160.0

        },
        new Tank{
            ProductId = 7,
            Brand = "ScubaPro",
            Volume = "12 liter",
            Price = 170.0

        },
        new Tank{
            ProductId = 8,
            Brand = "ScubaPro",
            Volume = "15 liter",
            Price = 180.0
        },
        new RegulatorSet{
            ProductId = 9,
            Brand = "ScubaPro",
            FirstStage = "MK25EVO",
            SecondStage = "S600",
            Octopus = "R105",
            Price = 125.0
        },
        new RegulatorSet{
            ProductId = 10,
            Brand = "ScubaPro",
            FirstStage = "MK17EVO",
            SecondStage = "C370",
            Octopus = "R095",
            Price = 100.0
        },
        new RegulatorSet{
            ProductId = 11,
            Brand = "ScubaPro",
            FirstStage = "MK25EVO BT",
            SecondStage = "A700 Carbon BT",
            Octopus = "S270",
            Price = 150.0
        },
        new Fin{
            ProductId = 12,
            Brand = "ScubaPro",
            Model = "Jet Fin",
            Size = "XS,S,M,L,XL",
            Price = 50.0
        },
        new Fin{
            ProductId = 13,
            Brand = "ScubaPro",
            Model = "GO Travel",
            Size = "XS,S,M,L,XL",
            Price = 50.0
        },
        new Fin{
            ProductId = 14,
            Brand = "ScubaPro",
            Model = "Seawing Supernova",
            Size = "XS,S,M,L,XL",
            Price = 60.0
        },
        new Fin{
            ProductId = 15,
            Brand = "Seac",
            Model = "Propulsion",
            Size = "XS,S,M,L,XL",
            Price = 50.0
        },
        new Fin{
            ProductId = 16,
            Brand = "Seac",
            Model = "ALA",
            Size = "XS,S,M,L,XL",
            Price = 50.0
        },
        new Fin{
            ProductId = 17,
            Brand = "Fourth Element",
            Model = "Tech",
            Size = "XS,S,M,L,XL",
            Price = 75.0
        },
        new Fin{
            ProductId = 18,
            Brand = "Fourth Element",
            Model = "Rec Fin",
            Size = "XS,S,M,L,XL",
            Price = 80.0
        },

        new DivingSuit
{
            ProductId = 19,
            Brand = "Scubapro",
            Model = "Definition",
            Size = "XS,S,N,L,XL",
            Type = "Våddragt",
            Gender = "Herre/Dame",
            Thickness = "3 mm",
            Price = 100
        },
new DivingSuit
{
            ProductId = 20,
            Brand = "Scubapro",
            Model = "Definition",
            Size = "XS,S,N,L,XL",
            Type = "Våddragt",
            Gender = "Herre/Dame",
            Thickness = "5 mm",
            Price = 100
        },
new DivingSuit
{
            ProductId = 21,
            Brand = "Scubapro",
            Model = "Definition",
            Size = "XS,S,N,L,XL",
            Type = "Våddragt",
            Gender = "Herre/Dame",
            Thickness = "7 mm",
            Price = 100
        },
new DivingSuit
{
            ProductId = 22,
            Brand = "Waterproof",
            Model = "W5",
            Size = "XS,S,N,L,XL",
            Type = "Våddragt",
            Gender = "Herre/Dame",
            Thickness = "3.5 mm",
            Price = 100
        },
new DivingSuit
{
            ProductId = 23,
            Brand = "Fourth Element",
            Model = "Proteus",
            Size = "XS,S,N,L,XL",
            Type = "Våddragt",
            Gender = "Herre/Dame",
            Thickness = "5 mm",
            Price = 120
        },
new DivingSuit
{
            ProductId = 24,
            Brand = "Scubapro",
            Model = "Exodry 4.0",
            Size = "XS,S,N,L,XL",
            Type = "Tørdragt",
            Gender = "Herre/Dame",
            Thickness = " ",
            Price = 300
        },
new DivingSuit
{
            ProductId = 25,
            Brand = "Waterproof",
            Model = "D7 Evo",
            Size = "XS,S,N,L,XL",
            Type = "Tørdragt",
            Gender = "Herre/Dame",
            Thickness = " ",
            Price = 320
        },
    new DivingSuit
        {
            ProductId = 26,
            Brand = "Santi",
            Model = "E.Lite plus",
            Size = "XS,S,N,L,XL",
            Type = "Tørdragt",
            Gender = "Herre/Dame",
            Thickness = " ",
            Price = 350
        },
        new Snorkel{
            ProductId = 27,
            Brand = "Scubapro",
            Model = "Ghost",
            Price = 50
        },
    new Snorkel{
            ProductId = 28,
            Brand = "Scubapro",
            Model = "D-Mask",
            Price = 60
        },
    new Snorkel{
            ProductId = 29,
            Brand = "Scubapro",
            Model = "Spectra Mini",
            Price = 50
        },
    new Snorkel{
            ProductId = 30,
            Brand = "Scubapro",
            Model = "Crystal VU",
            Price = 75
        },
    new Snorkel{
            ProductId = 31,
            Brand = "Fourth Element",
            Model = "Scout Kontrast",
            Price = 75
        },
    new Snorkel{
            ProductId = 32,
            Brand = "Fourth Element",
            Model = "Scout Enhance",
            Price = 75.0
        },
    new Snorkel{
            ProductId = 33,
            Brand = "Tusa",
            Model = "Element",
            Price = 75.0
        },


    };

    }
}
