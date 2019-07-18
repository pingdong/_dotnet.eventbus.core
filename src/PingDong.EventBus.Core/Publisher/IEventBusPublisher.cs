using System.Threading.Tasks;
using PingDong.CleanArchitect.Core;

namespace PingDong.EventBus.Core
{
    public interface IEventBusPublisher
    {
        Task PublishAsync(IntegrationEvent @event);
    }
}
