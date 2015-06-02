using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesExample.Domain
{
    public class MessageSentEvent
    {
        public MessageSentEvent(Guid aggregateId, string message)
        {
            AggregateId = aggregateId;
            Message = message;
        }

        public Guid AggregateId { get; private set; }
        public string Message { get; private set; }
    }
}
