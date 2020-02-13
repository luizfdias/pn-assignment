using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ParkingBooking.Booking.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            return Ok(new { IsHealthy = true });
        }
    }
}
