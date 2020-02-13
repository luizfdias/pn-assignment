using Microsoft.AspNetCore.Mvc;
using ParkingBooking.Booking.Application.Abstractions;
using ParkingBooking.Booking.Application.ApiModels;
using System;
using System.Threading.Tasks;

namespace ParkingBooking.Booking.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ParkingBookingController : ControllerBase
    {
        private readonly IParkingBookingService _parkingBookingService;

        public ParkingBookingController(IParkingBookingService parkingBookingService)
        {
            _parkingBookingService = parkingBookingService ?? throw new ArgumentNullException(nameof(parkingBookingService));
        }

        [HttpPost]
        public async Task<IActionResult> BookParking([FromBody]BookParkingRequest request)
        {
            await _parkingBookingService.BookParking(request);

            return Accepted();
        }
    }
}
