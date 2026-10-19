using Microsoft.AspNetCore.Identity;
using Shared.Models;

namespace Shared.Data
{
    public class ApplicationUser : IdentityUser
    {
        public List<Booking>? Bookings { get; set; }
    }
}