using EventStore.ClientAPI;
using GesExample.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GesExample.Emition
{
    class Program
    {
        private static readonly IPEndPoint IntegrationTestTcpEndPoint = new IPEndPoint(IPAddress.Loopback, 1113);
        private static IEventStoreConnection _connection;
        private static GetEventStoreRepository _repo;

        static void Main(string[] args)
        {
            _connection = EventStoreConnection.Create(IntegrationTestTcpEndPoint);
            _connection.Connect();
            _repo = new GetEventStoreRepository(_connection);


            string line = string.Empty;
            do
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var message = new MessageAggregate(Guid.NewGuid(), line);
                    _repo.Save(message, Guid.NewGuid(), d => { });
                    Console.WriteLine("New message emmited : {0}", line);
                    Console.WriteLine();
                }

                Console.WriteLine("Type the message to emmit: ");
            }
            while ((line = Console.ReadLine()) != "exit");

            _connection.Close();
        }
    }
}
