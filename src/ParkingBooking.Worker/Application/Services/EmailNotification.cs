using Microsoft.Extensions.Logging;
using ParkingBooking.Worker.Application.Abstractions;
using System.Threading.Tasks;

namespace ParkingBooking.Worker.Application.Services
{
    public class EmailNotification : INotificationService<string>
    {
        private readonly ILogger<EmailNotification> _logger;

        public EmailNotification(ILogger<EmailNotification> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(string notification)
        {
            _logger.LogInformation(notification);

            return Task.CompletedTask;
        }
    }
}
