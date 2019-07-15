using System.Threading.Tasks;

namespace PingDong.EventBus
{
    public interface IEventBusPublisher
    {
        Task PublishAsync(IntegrationEvent @event);
    }
}
