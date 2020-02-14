using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ParkingBooking.Reports.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GarageController : ControllerBase
    {
        [HttpGet("{garageId}")]
        public async Task<IActionResult> Get(
            Guid garageId, 
            DateTime startDate, 
            DateTime endDate)
        {            
            return Ok(new 
            {
                data = new object[] { },
                message = "Feature not available yet"
            });
        }
    }
}
