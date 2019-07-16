using System;

namespace PingDong.EventBus
{
    public class IntegrationEvent
    {
        public IntegrationEvent() 
            : this(string.Empty, string.Empty)
        {

        }

        public IntegrationEvent(string correlationId)
            : this(string.Empty, correlationId)
        {
        }

        public IntegrationEvent(string requestId, string correlationId)
        {
            RequestId = requestId;
            CreationDate = DateTime.UtcNow;
            CorrelationId = correlationId;
        }

        public string RequestId  { get; set; }
        public string CorrelationId { get; set; }
        public DateTime CreationDate { get; }
    }
}
