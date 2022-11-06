using ExpanseTrackerDDD.DomainModelLayer.Events.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events.Implementations
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        public string _connectionString { get; private set; }
        public string _brokerName { get; private set; }
        public Dictionary<IIntegrationEvent, IIntegrationEventHandler> _subsManager { get; private set; }
        //public Dictionary<string, ICollection<IIntegrationEventHandler>> _handlers { get; private set; }
        //public string _queueName { get; private set; }
        //public ICollection<Type> _eventTypes { get; private set; }
        public EventBusRabbitMQ(string connectionString, string brokerName, Dictionary<string, ICollection<IIntegrationEventHandler>> handlers, string queueName, ICollection<Type> eventTypes)
        {
            _connectionString = connectionString;
            _brokerName = brokerName;
            //_handlers = handlers;
            //_queueName = queueName;
            //_eventTypes = eventTypes;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Publish<T>(T integrationEvent) where T : IIntegrationEvent
        {
            var eventName = integrationEvent.GetType().Name;
            var factory = new ConnectionFactory() { HostName = _connectionString };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: _brokerName,
                    type: "direct");
                string message = JsonConvert.SerializeObject(integrationEvent);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: _brokerName,
                    routingKey: eventName,
                    basicProperties: null,
                    body: body);
            }
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();

            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                using (var channel = _persistentConnection.CreateModel())
                {
                    channel.QueueBind(queue: _queueName,
                                        exchange: BROKER_NAME,
                                        routingKey: eventName);
                }
            }

            _subsManager.AddSubscription<T, TH>();
        }

        public void Unsubscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
        {
            throw new NotImplementedException();
        }

    }
}
