using System;

namespace PingDong.EventBus.Core
{
    public class IntegrationEvent
    {
        public IntegrationEvent() 
            : this(Guid.Empty, Guid.Empty, Guid.Empty)
        {

        }

        public IntegrationEvent(Guid requestId, Guid tenantId, Guid correlationId)
        {
            RequestId = requestId;
            CreationDate = DateTime.UtcNow;
            CorrelationId = correlationId;
            TenantId = tenantId;
        }

        public Guid RequestId  { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid TenantId { get; set; }
        public DateTime CreationDate { get; }
    }
}
