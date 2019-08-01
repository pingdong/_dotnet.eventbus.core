using System.Threading.Tasks;

namespace PingDong.EventBus.Core
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
