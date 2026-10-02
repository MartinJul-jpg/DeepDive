using System.ComponentModel.DataAnnotations;

namespace DeepDive.Models
{
    public class Confirmation
    {
        public int ConfirmationId { get; set; }
        public Booking? Booking { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Tlf { get; set; }
    }
}
