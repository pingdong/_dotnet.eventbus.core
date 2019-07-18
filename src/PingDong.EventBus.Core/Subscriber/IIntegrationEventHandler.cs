using System.Threading.Tasks;
using PingDong.CleanArchitect.Core;

namespace PingDong.EventBus.Core
{
    public interface IIntegrationEventHandler<in TIntegrationEvent>
        where TIntegrationEvent: IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }
}
