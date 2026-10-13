namespace Shared.DTOs
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public List<ProductDTO> ProductsDTOs { get; set; }
        public string? UserId { get; set; }
        public byte[]? RowVersion { get; set; }
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }
    }
}
