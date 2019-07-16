using System;

namespace PingDong.EventBus
{
    public class IntegrationEvent
    {
        public IntegrationEvent() 
            : this(Guid.NewGuid(), Guid.NewGuid())
        {

        }

        public IntegrationEvent(Guid requestId, Guid correlationId)
        {
            RequestId = requestId;
            CreationDate = DateTime.UtcNow;
            CorrelationId = correlationId;
        }

        public Guid RequestId  { get; set; }
        public Guid CorrelationId { get; set; }
        public DateTime CreationDate { get; }
    }
}
