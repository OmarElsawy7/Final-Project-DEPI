using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventHub.ViewModels
{
    public class CreateAndEditEventViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [StringLength(100)]
        public string? Category { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, StringLength(200)]
        public string Location { get; set; } = null!;

        [Precision(18, 2)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int TotalTickets { get; set; }
    }
}
