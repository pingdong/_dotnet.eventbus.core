using System.Threading.Tasks;

namespace PingDong.EventBus
{
    public interface IIntegrationEventHandler<in TIntegrationEvent>
        where TIntegrationEvent: IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }
}
