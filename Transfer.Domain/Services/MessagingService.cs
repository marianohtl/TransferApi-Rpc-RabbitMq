using RabbitMQ.Client;
using Transfer.Domain.Interfaces;
using System;
using System.Text;
using Transfer.Domain.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Transfer.Domain.Services
{
    public class MessagingService : IMessaging
    {
        private readonly string _hostname;
        private readonly string _sendQueue;
        private readonly string _replyQueue;

        private IConnection _connection;

        public MessagingService(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _sendQueue = rabbitMqOptions.Value.SendQueue;
            _replyQueue = rabbitMqOptions.Value.ReplyQueue;
            _hostname = rabbitMqOptions.Value.Hostname;

            CreateConnection();
        }
        public void SendTransfer(ClientTransfer transfer)
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _sendQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var json = JsonConvert.SerializeObject(transfer);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: _sendQueue, basicProperties: null, body: body);
                }
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }

  
    }
}
