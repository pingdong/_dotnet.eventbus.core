using System.Threading.Tasks;

namespace PingDong.EventBus.Core
{
    public interface IMessageDispatcher<in TMessage>
    {
        Task DispatchAsync(TMessage message);
    }
}
