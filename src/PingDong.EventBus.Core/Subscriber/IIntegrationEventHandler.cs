using System.Threading.Tasks;

namespace PingDong.EventBus.Core
{
    public interface IIntegrationEventHandler<in TIntegrationEvent>
        where TIntegrationEvent: IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }
}
