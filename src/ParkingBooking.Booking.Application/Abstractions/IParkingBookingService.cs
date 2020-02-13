using ParkingBooking.Booking.Application.ApiModels;
using System.Threading.Tasks;

namespace ParkingBooking.Booking.Application.Abstractions
{
    public interface IParkingBookingService
    {
        Task BookParking(BookParkingRequest request);
    }
}
