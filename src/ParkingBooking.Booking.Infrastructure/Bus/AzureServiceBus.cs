using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ParkingBooking.Booking.Api.Application.Abstractions;
using ParkingBooking.Booking.Domain.Abstractions;
using System.Text;
using System.Threading.Tasks;

namespace ParkingBooking.Booking.Infrastructure.Bus
{
    public class AzureServiceBus : IServiceBus
    {
        private readonly string _connectionString;

        public AzureServiceBus(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new System.ArgumentException("message", nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        public Task RaiseEvent<T>(T @event, string queueKey) where T : Event
        {
            return SendAsync(@event, queueKey);
        }

        public Task SendCommand<T>(T command, string topicKey) where T : Command
        {
            return SendAsync(command, topicKey);
        }

        private Task SendAsync(object message, string key)
        {
            var queueClient = new QueueClient(_connectionString, key);

            var jsonMessage = JsonConvert.SerializeObject(message);

            var streamMessage = new Message(Encoding.UTF8.GetBytes(jsonMessage));

            return queueClient.SendAsync(streamMessage);
        }
    }
}
