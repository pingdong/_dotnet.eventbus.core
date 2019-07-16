using System;

namespace PingDong.EventBus
{
    public class IntegrationEvent
    {
        public IntegrationEvent() : this(Guid.NewGuid())
        {

        }

        public IntegrationEvent(Guid correlationId)
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            CorrelationId = correlationId;
        }

        public Guid Id  { get; }
        public Guid CorrelationId { get; set; }
        public DateTime CreationDate { get; }
    }
}
