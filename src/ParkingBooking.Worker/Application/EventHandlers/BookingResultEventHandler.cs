using System;
using System.Threading.Tasks;
using ParkingBooking.Booking.Domain.Abstractions;
using ParkingBooking.Booking.Domain.Events;
using ParkingBooking.Worker.Application.Abstractions;

namespace ParkingBooking.Worker.Application.EventHandlers
{
    public class BookingResultEventHandler : IEventHandler
    {
        private readonly INotificationService<string> _notificationService;

        public BookingResultEventHandler(INotificationService<string> notificationService)
        {
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        public Task Handle(Event @event)
        {
            switch (@event)
            {
                case ParkingAlreadyTaken parkingTaken:
                    _notificationService.SendAsync($"No availability was found from {parkingTaken.From} to {parkingTaken.To} - Car: {parkingTaken.LicensePlate}");
                    break;
                case ParkingBooked parkingBooked:
                    _notificationService.SendAsync($"Parking booked! Reference {parkingBooked.Reference} from {parkingBooked.From} to {parkingBooked.To} - Car: {parkingBooked.LicensePlate}");
                    break;
                default:
                    //Todo: Log event not recognized
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
