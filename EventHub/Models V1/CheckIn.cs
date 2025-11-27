using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.ModelsV1
{
    public class CheckIn
    {
        [Key]
        public int CheckInID { get; set; }

        [Required]
        [ForeignKey("Event")]
        public int EventID { get; set; }

        [Required]
        [ForeignKey("Ticket")]
        public int TicketID { get; set; }

        [Required]
        [Display(Name = "Check-in Time")]
        [DataType(DataType.DateTime)]
        public DateTime CheckInTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Scanned By user is required.")]
        [StringLength(450)]
        [Display(Name = "Scanned By")]
        public string ScannedBy { get; set; }

        // Navigation Properties
        public virtual Event? Event { get; set; }
        public virtual Ticket? Ticket { get; set; }
    }
}