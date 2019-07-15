using System.Threading.Tasks;

namespace PingDong.EventBus.Core
{
    public interface IEventBusMessageDispatcher<in TMessage>
    {
        Task DispatchAsync(TMessage message);
    }
}
