using CS_PostgreExample.Data;
using CS_PostgreExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CS_PostgreExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class DriversController : ControllerBase
    {
        private ApiDbContext _context;
        private readonly ILogger<DriversController> _logger;

        public DriversController(ILogger<DriversController> logger, ApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetAllDrivers")]
        public async Task<IActionResult> GetAll()
        {
            var driver = new Driver()
            {
                DriverNumber = 44,
                Name = "Michael Schumacher"
            };

            _context.Add(driver);
            await _context.SaveChangesAsync();
            var allDrivers = await _context.Drives.ToListAsync();
            return Ok(allDrivers);
        }
    }
}