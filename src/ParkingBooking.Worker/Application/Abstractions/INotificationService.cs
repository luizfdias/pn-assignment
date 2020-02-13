using System.Threading.Tasks;

namespace ParkingBooking.Worker.Application.Abstractions
{
    public interface INotificationService<TNotification>
    {
        Task SendAsync(TNotification notification);
    }
}
