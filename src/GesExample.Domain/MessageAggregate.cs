using CommonDomain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesExample.Domain
{
    public class MessageAggregate : AggregateBase
    {
        public string Message { get; private set; } 

        public MessageAggregate(Guid aggregateId, string message) : this()
        {
            RaiseEvent(new MessageSentEvent(aggregateId, message));
        }

        private MessageAggregate()
        {
            Register<MessageSentEvent>(e => { Id = e.AggregateId; Message = e.Message; });
        }

        public int AppliedEventCount { get; private set; }
    }
}
