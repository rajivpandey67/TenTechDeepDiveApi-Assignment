using System;
using System.ComponentModel.DataAnnotations;

namespace TenTechDeepDiveApi.Models
{
    public class InventoryItem
    {
        [Key] // Primary Key
        public int Id { get; set; } // Or generate a GUID
        public string Title { get; set; }
        public string Description { get; set; }
        public int RemainingCount { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
