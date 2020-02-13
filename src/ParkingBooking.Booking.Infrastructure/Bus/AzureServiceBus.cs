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
                throw new ArgumentException(nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        public Task ListenCommand<T>(Func<T, Task> onReceivedAsync, string queueKey) where T : Command
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

        public Task ListenEvent<T>(Func<T, Task> onReceivedAsync, string topicKey, string subscriptionName) where T : Event
        {
            var subscriptionClient = new SubscriptionClient(_connectionString, topicKey, subscriptionName);

            var messageHandlerOptions = CreateMessageHandlerOptions();

            subscriptionClient.RegisterMessageHandler(async (msg, token) =>
            {
                var content = Encoding.UTF8.GetString(msg.Body);

                var message = JsonConvert.DeserializeObject<T>(content);

                await onReceivedAsync(message);

                await subscriptionClient.CompleteAsync(msg.SystemProperties.LockToken);

            }, messageHandlerOptions);

            return Task.CompletedTask;
        }

        public Task RaiseEvent<T>(T @event, string topicKey) where T : Event
        {
            var topicClient = new TopicClient(_connectionString, topicKey);

            var jsonMessage = JsonConvert.SerializeObject(@event);

            var streamMessage = new Message(Encoding.UTF8.GetBytes(jsonMessage));

            return topicClient.SendAsync(streamMessage);
        }

        public Task SendCommand<T>(T command, string topicKey) where T : Command
        {
            var queueClient = new QueueClient(_connectionString, topicKey);

            var jsonMessage = JsonConvert.SerializeObject(command);

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
