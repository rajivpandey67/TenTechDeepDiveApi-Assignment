using Microsoft.EntityFrameworkCore;
using TenTechDeepDiveApi.Models;

namespace TenTechDeepDiveApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
