using ParkingBooking.Booking.Domain.Abstractions;
using System.Threading.Tasks;

namespace ParkingBooking.Worker.Application.Abstractions
{
    public interface IEventHandler
    {
        Task Handle(Event @event);
    }
}
