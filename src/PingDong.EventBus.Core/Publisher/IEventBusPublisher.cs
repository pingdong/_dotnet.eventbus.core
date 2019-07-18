using System.Threading.Tasks;

namespace PingDong.EventBus.Core
{
    public interface IEventBusPublisher
    {
        Task PublishAsync(IntegrationEvent @event);
    }
}
