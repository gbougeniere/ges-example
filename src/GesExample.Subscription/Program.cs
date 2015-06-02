using CommonDomain;
using CommonDomain.Core;
using EventStore.ClientAPI;
using GesExample.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GesExample.Subscription
{
    // Start this Program and wait for events
    class Program
    {
        static void Main(string[] args)
        {
            // * Bind to Events
            var er = new EventRouter();
            er.RegisteredRoutes.Register<MessageSentEvent>(
                //Do something with the given event
                e => Console.WriteLine("Received message: {0}", e.Message )
            );



            const string STREAM = "$et-MessageSentEvent";
            const int DEFAULTPORT = 1113;
            //uncommet to enable verbose logging in client.
            var settings = ConnectionSettings.Create();//.EnableVerboseLogging().UseConsoleLogger();
            using (var conn = EventStoreConnection.Create(settings, new IPEndPoint(IPAddress.Loopback, DEFAULTPORT)))
            {
                conn.ConnectAsync().Wait();
                //Note the subscription is subscribing from the beginning every time. You could also save
                //your checkpoint of the last seen event and subscribe to that checkpoint at the beginning.
                //If stored atomically with the processing of the event this will also provide simulated
                //transactional messaging.

                // * Subscribe to Stream(s)
                var sub = conn.SubscribeToStreamFrom(STREAM, StreamPosition.Start,true,
                    (_, x) =>
                    {
                        er.RegisteredRoutes.Dispatch(GetEventStoreRepository.DeserializeEvent(x.Event.Metadata, x.Event.Data));
                    });
                Console.WriteLine("waiting for events. press enter to exit");
                Console.ReadLine();
            }
        }
    }

    public class EventRouter
    {
        private IRouteEvents registeredRoutes;
        public IRouteEvents RegisteredRoutes
        {
            get
            {
                return registeredRoutes ?? (registeredRoutes = new ConventionEventRouter());
            }
            set
            {
                if (value == null)
                    throw new InvalidOperationException("AggregateBase must have an event router to function");

                registeredRoutes = value;
            }
        }

        
    
    }
        
}
