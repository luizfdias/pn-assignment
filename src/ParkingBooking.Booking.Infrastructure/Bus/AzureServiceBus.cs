using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ParkingBooking.Booking.Api.Application.Abstractions;
using ParkingBooking.Booking.Domain.Abstractions;
using System;
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

        public Task ListenCommand<T>(Func<T, Task> onReceivedAsync, string queueKey) where T : Command
        {
            return Listen<T>(onReceivedAsync, queueKey);
        }

        public Task ListenEvent<T>(Func<T, Task> onReceivedAsync, string topicKey) where T : Event
        {
            return Listen<T>(onReceivedAsync, topicKey);
        }

        public Task RaiseEvent<T>(T @event, string queueKey) where T : Event
        {
            return SendAsync(@event, queueKey);
        }

        public Task SendCommand<T>(T command, string topicKey) where T : Command
        {
            return SendAsync(command, topicKey);
        }

        private Task Listen<T>(Func<T, Task> onReceivedAsync, string queueKey)
        {
            var queueClient = new QueueClient(_connectionString, queueKey);

            var messageHandlerOptions = CreateMessageHandlerOptions();

            queueClient.RegisterMessageHandler(async (msg, token) =>
            {
                var content = Encoding.UTF8.GetString(msg.Body);

                var message = JsonConvert.DeserializeObject<T>(content);

                await onReceivedAsync(message);

                await queueClient.CompleteAsync(msg.SystemProperties.LockToken);

            }, messageHandlerOptions);

            return Task.CompletedTask;
        }

        private Task SendAsync(object message, string key)
        {
            var queueClient = new QueueClient(_connectionString, key);

            var jsonMessage = JsonConvert.SerializeObject(message);

            var streamMessage = new Message(Encoding.UTF8.GetBytes(jsonMessage));

            return queueClient.SendAsync(streamMessage);
        }

        private MessageHandlerOptions CreateMessageHandlerOptions()
        {
            var messageHandlerOptions = new MessageHandlerOptions((ex) =>
            {
                //Todo: Log exception 
                return Task.CompletedTask;
            })
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            return messageHandlerOptions;
        }        
    }
}
