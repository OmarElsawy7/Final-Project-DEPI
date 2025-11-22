using System.ComponentModel.DataAnnotations;
namespace EventHub.Models
{
    public class Pending
    {
        public int PurchaseID { get; set; }
        public int EventID { get; set; }
        public string BuyerName { get; set; }
        public string BuyerEmail { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation Properties
        public Event Event { get; set; }
        public User Buyer { get; set; }
    }

}
