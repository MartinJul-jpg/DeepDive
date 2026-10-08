using Shared.DTOs;
using Shared.Models;

namespace Shared.Services
{
    public interface IDTO_Service
    {
        ProductDTO ProductToDTO(Product product);
        Product DTO_ToProduct(ProductDTO DTO);
        BookingDTO BookingToDTO(Booking booking);
        Booking DTO_ToBooking(BookingDTO DTO);
        PackageDTO PackageToDTO(Package package);
        Package DTO_ToPackage(PackageDTO DTO);
    }
}
