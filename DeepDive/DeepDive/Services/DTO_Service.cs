using DeepDive.Models;
using DeepDive.ReceivedDTO;

namespace DeepDive.Services
{
    public class DTO_Service : IDTO_Service
    {
        public ProductDTO ProductToDTO(Product product)
        {
            ProductDTO DTO = new ProductDTO();

            DTO.ProductId = product.ProductId;
            DTO.Price = product.Price;
            DTO.Brand = product.Brand;
            DTO.Amount = product.Amount;
            DTO.ImgPath = product.ImgPath;
            DTO.RowVersion = product.RowVersion;
            DTO.ProductType = product.GetType().ToString();

            switch (DTO.ProductType)
            {
                case "BCD":
                    DTO.Model = (product as BCD).Model;
                    DTO.Size = (product as BCD).Size;
                    break;
                case "DivingSuit":
                    DTO.Model = (product as DivingSuit).Model;
                    DTO.Size = (product as DivingSuit).Size;
                    DTO.Type = (product as DivingSuit).Type;
                    DTO.Gender = (product as DivingSuit).Gender;
                    DTO.Thickness = (product as DivingSuit).Thickness;
                    break;
                case "Fin":
                    DTO.Model = (product as Fin).Model;
                    DTO.Size = (product as Fin).Size;
                    break;
                case "RegulatorSet":
                    DTO.FirstStage = (product as RegulatorSet).FirstStage;
                    DTO.SecondStage = (product as RegulatorSet).SecondStage;
                    DTO.Octopus = (product as RegulatorSet).Octopus;
                    break;
                case "Snorkel":
                    DTO.Model = (product as Snorkel).Model;
                    break;
                case "Tank":
                    DTO.Volume = (product as Tank).Volume;
                    break;
            }

            return DTO;
        }

        public Product DTO_ToProduct(ProductDTO DTO)
        {
            Product product = null;

            switch (DTO.ProductType)
            {
                case "BCD":
                    BCD bcd = new BCD();
                    bcd.Model = DTO.Model;
                    bcd.Size = DTO.Size;
                    product = bcd;
                    break;
                case "DivingSuit":
                    DivingSuit divingSuit = new DivingSuit();
                    divingSuit.Model = DTO.Model;
                    divingSuit.Size = DTO.Size;
                    divingSuit.Type = DTO.Type;
                    divingSuit.Gender = DTO.Gender;
                    divingSuit.Thickness = DTO.Thickness;
                    product = divingSuit;
                    break;
                case "Fin":
                    Fin fin = new Fin();
                    fin.Model = DTO.Model;
                    fin.Size = DTO.Size;
                    product = fin;
                    break;
                case "RegulatorSet":
                    RegulatorSet regulatorSet = new RegulatorSet();
                    regulatorSet.FirstStage = DTO.FirstStage;
                    regulatorSet.SecondStage = DTO.SecondStage;
                    regulatorSet.Octopus = DTO.Octopus;
                    product = regulatorSet;
                    break;
                case "Snorkel":
                    Snorkel snorkel = new Snorkel();
                    snorkel.Model = DTO.Model;
                    product = snorkel;
                    break;
                case "Tank":
                    Tank tank = new Tank();
                    tank.Volume = DTO.Volume;
                    product = tank;
                    break;
            }

            product.ProductId = DTO.ProductId;
            product.Price = DTO.Price;
            product.Brand = DTO.Brand;
            product.Amount = DTO.Amount;
            product.ImgPath = DTO.ImgPath;
            product.RowVersion = DTO.RowVersion;

            return product;
        }

        public BookingDTO BookingToDTO(Booking booking)
        {
            BookingDTO DTO = new BookingDTO();

            DTO.BookingId = booking.BookingId;
            DTO.UserId = booking.UserId;
            DTO.RowVersion = booking.RowVersion;
            DTO.Start = booking.Start;
            DTO.End = booking.End;

            DTO.Products = booking.Product_Bookings?.Select(pb => ProductToDTO(pb.Product)).ToList() ?? new List<ProductDTO>();

            return DTO;
        }

        public Booking DTO_ToBooking(BookingDTO DTO)
        {
            Booking booking = new Booking();

            booking.BookingId = DTO.BookingId;
            booking.UserId = DTO.UserId;
            booking.RowVersion = DTO.RowVersion;
            booking.Start = DTO.Start;
            booking.End = DTO.End;

            booking.Product_Bookings = DTO.Products?
                .Select(productDTO => new Product_Booking()
                {
                    ProductId = productDTO.ProductId,
                    BookingId = DTO.BookingId
                })
                .ToList() ?? new List<Product_Booking>();

            return booking;
        }

        public PackageDTO PackageToDTO(Package package)
        {
            PackageDTO DTO = new PackageDTO();

            DTO.PackageId = package.PackageId;

            DTO.Products = package.Package_Products?.Select(pp => ProductToDTO(pp.Product)).ToList() ?? new List<ProductDTO>();

            return DTO;
        }

        public Package DTO_ToPackage(PackageDTO DTO)
        {
            Package package = new Package();

            package.PackageId = DTO.PackageId;

            package.Package_Products = DTO.Products?
                .Select(productDTO => new Package_Product()
                {
                    ProductId = productDTO.ProductId,
                    PackageId = DTO.PackageId
                })
                .ToList() ?? new List<Package_Product>();

            return package;
        }
    }
}
