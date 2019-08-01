using System;
using System.Collections.Generic;
using System.Linq;
using PingDong.CleanArchitect.Core;

namespace PingDong.EventBus.Core
{
    public partial class SubscriptionsManager : ISubscriptionsManager
    {
        #region Variables

        private readonly Dictionary<string, List<Subscriber>> _handlers;
        private readonly Dictionary<string, Type> _fixedEventTypes;

        private readonly string _eventTypeSuffix;

        #endregion

        #region ctor

        public SubscriptionsManager(string eventTypeSuffix = "IntegrationEvent")
        {
            _handlers = new Dictionary<string, List<Subscriber>>();
            _fixedEventTypes = new Dictionary<string, Type>();
            _eventTypeSuffix = eventTypeSuffix;
        }

        #endregion

        #region ISubscriptionsManager
        
        public void AddSubscriber<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>
        {
            AddSubscriber(typeof(TEvent), typeof(THandler));
        }

        public void AddSubscriber(Type eventType, Type eventHandler)
        {
            if (eventType == null)
                throw new ArgumentNullException(nameof(eventType));
            if (eventHandler == null)
                throw new ArgumentNullException(nameof(eventHandler));

            var eventName = GetEventName(eventType);

            _fixedEventTypes.Add(eventName, eventType);

            AddSubscriber(eventName, eventHandler, isDynamic:false);
        }

        public void AddSubscriber<THandler>(string eventName)
            where THandler : IDynamicIntegrationEventHandler
        {
            AddSubscriber(eventName, typeof(THandler));
        }

        public void AddSubscriber(string eventName, Type handler)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                throw new ArgumentNullException(nameof(eventName));

            eventName = GetEventName(eventName);

            AddSubscriber(eventName, handler, isDynamic: true);
        }

        public void RemoveSubscriber<TEvent, THandler>()
            where THandler : IIntegrationEventHandler<TEvent>
            where TEvent : IntegrationEvent
        {
            var eventName = GetEventName<TEvent>();

            RemoveSubscriberInternal<THandler>(eventName);
        }

        public void RemoveSubscriber<THandler>(string eventName)
            where THandler : IDynamicIntegrationEventHandler
        {
            RemoveSubscriberInternal<THandler>(eventName);
        }

        public IList<Subscriber> GetSubscribers<T>() where T : IntegrationEvent
        {
            var key = GetEventName<T>();

            return GetSubscribers(key);
        }

        public IList<Subscriber> GetSubscribers(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                throw new ArgumentNullException(nameof(eventName));

            if (!_handlers.ContainsKey(eventName))
                return null;

            return _handlers[eventName];
        }

        public bool HasSubscribers<T>() where T : IntegrationEvent
        {
            var key = GetEventName<T>();

            return HasSubscribers(key);
        }

        public bool HasSubscribers(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                return false;

            eventName = GetEventName(eventName);

            return _handlers.ContainsKey(eventName);
        }

        public Type GetEventType(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                return null;

            eventName = GetEventName(eventName);

            return !_fixedEventTypes.ContainsKey(eventName) ? null : _fixedEventTypes[eventName];
        }

        public Type GetEventType<T>() where T : IntegrationEvent
        {
            var eventName = GetEventName<T>();

            return !_fixedEventTypes.ContainsKey(eventName) ? null : _fixedEventTypes[eventName];
        }
        
        public void Clear()
        {
            _handlers.Clear();
            _fixedEventTypes.Clear();
        }

        public bool IsDynamic(string eventName)
        {
            if (!HasSubscribers(eventName))
                throw new IndexOutOfRangeException();

            return !_fixedEventTypes.ContainsKey(eventName);
        }

        #endregion

        #region Private Methods

        private string GetEventName<T>()
        {
            return typeof(T).Name.Replace(_eventTypeSuffix, "");
        }

        private string GetEventName(string eventName)
        {
            return eventName.Replace(_eventTypeSuffix, "");
        }

        private string GetEventName(Type eventType)
        {
            return eventType.Name.Replace(_eventTypeSuffix, "");
        }

        private void RemoveSubscriberInternal<THandler>(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                throw new ArgumentNullException(nameof(eventName));

            eventName = GetEventName(eventName);

            var subscriber = _handlers[eventName].FirstOrDefault(s => s.HandlerType == typeof(THandler));
            if (subscriber == null)
                return;

            // Remove SubscriptionInfo from List
            _handlers[eventName].Remove(subscriber);

            if (_handlers[eventName].Any())
                return;

            // If there is no handler for this event,
            //    remove the event from handler
            _handlers.Remove(eventName);
            _fixedEventTypes.Remove(eventName);
        }

        private void AddSubscriber(string eventName, Type handlerType, bool isDynamic)
        {
            // If this is the first handler for this event,
            //    a new handler list needs to be created
            if (!HasSubscribers(eventName))
                _handlers.Add(eventName, new List<Subscriber>());
            
            // Can't have the same handler twice
            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
                throw new ArgumentException($"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));

            _handlers[eventName].Add(
                isDynamic 
                    ? Subscriber.Dynamic(handlerType) 
                    : Subscriber.Typed(handlerType)
            );
        }

        #endregion
    }
}
