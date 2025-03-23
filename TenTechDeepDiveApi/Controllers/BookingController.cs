using Microsoft.AspNetCore.Mvc;
using TenTechDeepDiveApi.Data;
using TenTechDeepDiveApi.Models;
using System;
using System.Linq;

namespace TenTechDeepDiveApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private const int MAX_BOOKINGS = 2; // Defined constant

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("book")]
        public IActionResult BookItem(int memberId, int inventoryItemId)
        {
            var member = _context.Members.Find(memberId);
            var inventoryItem = _context.InventoryItems.Find(inventoryItemId);

            if (member == null || inventoryItem == null)
            {
                return NotFound("Member or Inventory item not found.");
            }

            if (member.BookingCount >= MAX_BOOKINGS)
            {
                return BadRequest($"Member {member.Name} {member.Surname} has reached the maximum booking limit ({MAX_BOOKINGS}).");
            }

            if (inventoryItem.RemainingCount <= 0)
            {
                return BadRequest($"Inventory item '{inventoryItem.Title}' is out of stock.");
            }

            // Create Booking
            var booking = new Booking
            {
                MemberId = memberId,
                InventoryItemId = inventoryItemId
            };
            _context.Bookings.Add(booking);

            // Update Member's Booking Count and Inventory Remaining Count
            member.BookingCount++;
            inventoryItem.RemainingCount--;

            _context.SaveChanges(); // Save all changes in one transaction

            return Ok(new { message = "Booking successful.", bookingReference = booking.BookingReference });
        }
        // ... inside BookingController.cs ...

        [HttpPost("cancel")]
        public IActionResult CancelBooking(Guid bookingReference)
        {
            var booking = _context.Bookings
                .FirstOrDefault(b => b.BookingReference == bookingReference);

            if (booking == null)
            {
                return NotFound($"Booking with reference '{bookingReference}' not found.");
            }

            // Retrieve related Member and Inventory Item
            var member = _context.Members.Find(booking.MemberId);
            var inventoryItem = _context.InventoryItems.Find(booking.InventoryItemId);

            if (member != null && inventoryItem != null) // Defensive checks incase of DB inconsistencies
            {
                // Revert booking count and inventory count
                member.BookingCount--;
                inventoryItem.RemainingCount++;

                _context.Bookings.Remove(booking); // Remove the booking record

                _context.SaveChanges();
                return Ok(new { message = $"Booking with reference '{bookingReference}' cancelled successfully." });
            }
            else
            {
                // Handle scenario where related Member or InventoryItem is missing (data inconsistency)
                return StatusCode(500, "Error: Related member or inventory item not found for this booking. Booking cannot be fully cancelled.");
            }
        }

    }

}
