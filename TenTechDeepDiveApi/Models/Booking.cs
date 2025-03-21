using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenTechDeepDiveApi.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public DateTime BookingDateTime { get; set; } = DateTime.UtcNow; // Record booking time
        public Guid BookingReference { get; set; } = Guid.NewGuid();  // Unique booking reference
        // Foreign Keys to link to Member and InventoryItem
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [ForeignKey("InventoryItem")]
        public int InventoryItemId { get; set; }
        public InventoryItem InventoryItem { get; set; }
    }
}
