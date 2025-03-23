using Microsoft.AspNetCore.Mvc;
using TenTechDeepDiveApi.Services;

namespace TenTechDeepDiveApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataUploadController : ControllerBase
    {
        private readonly CsvDataService _csvDataService;

        public DataUploadController(CsvDataService csvDataService)
        {
            _csvDataService = csvDataService;
        }

        [HttpPost("upload-members-csv")]
        public IActionResult UploadMembersCsv(IFormFile file)
        {
            if (file == null || file.Length == 0 || !file.FileName.ToLower().EndsWith(".csv"))
            {
                return BadRequest("Invalid CSV file.");
            }

            using (var stream = file.OpenReadStream())
            {
                _csvDataService.SeedMembersFromCsv(stream);
            }

            return Ok("Members CSV data uploaded and seeded successfully.");
        }

        [HttpPost("upload-inventory-csv")]
        public IActionResult UploadInventoryCsv(IFormFile file)
        {
            if (file == null || file.Length == 0 || !file.FileName.ToLower().EndsWith(".csv"))
            {
                return BadRequest("Invalid CSV file.");
            }

            using (var stream = file.OpenReadStream())
            {
                _csvDataService.SeedInventoryFromCsv(stream);
            }

            return Ok("Inventory CSV data uploaded and seeded successfully.");
        }
    }
}
