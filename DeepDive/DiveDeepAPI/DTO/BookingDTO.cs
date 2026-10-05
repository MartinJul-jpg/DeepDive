using DiveDeepAPI.Data;
using DiveDeepAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DiveDeepAPI.Models
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        [ValidateNever]
        public List<int> ProductId { get; set; } = new List<int>();
        public string? UserId { get; set; }
        //For what reason do we have this
        public ApplicationUser? User { get; set; }
        public bool Finalized { get; set; }

        public byte[]? RowVersion { get; set; }

        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }
    }
}
