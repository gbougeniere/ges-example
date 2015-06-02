using System;

namespace GesExample.Domain
{
    public class TestAggregateCreated
    {
        public TestAggregateCreated(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

        public Guid AggregateId { get; private set; }
    }
}