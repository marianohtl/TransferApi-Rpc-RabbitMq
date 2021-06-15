using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net;
using System.Text;

namespace Transfer.Rpc
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "ReplyQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (ch, ea) =>
                {
                    var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var clientTransferData = JsonConvert.DeserializeObject<ClientTransfer>(content);


                  

                };

            }



          
        }


        public void Credit(ClientTransfer clientTransfer)
        {
            if (clientTransfer != null)
            {
                var accountTransfer = new AccountTransfer { AccountNumber = clientTransfer.AccountDestination, Type = "Credit", Value = clientTransfer.Value };
                var body = JsonConvert.SerializeObject(accountTransfer);

                var request = WebRequest.CreateHttp("http://localhost:5000/api/Account");

                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = body.Length;

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    //return response;
                }
            }
        }

        public void Debit(ClientTransfer clientTransfer)
        {
            var accountTransfer = new AccountTransfer { AccountNumber = clientTransfer.AccountDestination, Type = "Debit", Value = clientTransfer.Value };
            var body = JsonConvert.SerializeObject(accountTransfer);

            var request = WebRequest.CreateHttp("http://localhost:5000/api/Account");

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = body.Length;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                //return response;
            }
        
            }

    }
}
