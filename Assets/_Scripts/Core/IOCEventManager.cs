using System;
using System.Collections.Generic;

namespace ioc.IOCStudents.Core
{
    public struct IOCGameEvent
    {
        public string m_eventName;
        public string m_eventData;

        public IOCGameEvent(string newName, string newData)
        {
            m_eventName = newName;
            m_eventData = newData;
        }
        static IOCGameEvent e;
        public static void Trigger(string newName, string newData)
        {
            e.m_eventName = newName;
            e.m_eventData = newData;
            IOCEventManager.TriggerEvent(e);
        }
    }


    public static class IOCEventManager
    {
        private static Dictionary<Type, List<IOCEventListenerBase>> m_subscribersList;

        static IOCEventManager()
        {
            m_subscribersList = new Dictionary<Type, List<IOCEventListenerBase>>();
        }

        public static void AddListener<IOCEvent>(IOCEventListener<IOCEvent> listener) where IOCEvent : struct
        {
            Type eventType = typeof(IOCEvent);

            if (!m_subscribersList.ContainsKey(eventType))
                m_subscribersList[eventType] = new List<IOCEventListenerBase>();

            if (!SubscriptionExists(eventType, listener))
                m_subscribersList[eventType].Add(listener);
        }

        public static void RemoveListener<IOCEvent>(IOCEventListener<IOCEvent> listener) where IOCEvent : struct
        {
            Type eventType = typeof(IOCEvent);

            if (!m_subscribersList.ContainsKey(eventType))
            {
                return;
            }

            List<IOCEventListenerBase> subscriberList = m_subscribersList[eventType];
            bool listenerFound;
            listenerFound = false;

            for (int i = 0; i < subscriberList.Count; i++)
            {
                if (subscriberList[i] == listener)
                {
                    subscriberList.Remove(subscriberList[i]);
                    listenerFound = true;

                    if (subscriberList.Count == 0)
                        m_subscribersList.Remove(eventType);

                    return;
                }
            }

        }

        public static void TriggerEvent<IOCEvent>(IOCEvent newEvent) where IOCEvent : struct
        {
            List<IOCEventListenerBase> list;
            if (!m_subscribersList.TryGetValue(typeof(IOCEvent), out list))
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                (list[i] as IOCEventListener<IOCEvent>).OnIOCEvent(newEvent);
            }
        }

        private static bool SubscriptionExists(Type type, IOCEventListenerBase receiver)
        {
            List<IOCEventListenerBase> receivers;

            if (!m_subscribersList.TryGetValue(type, out receivers)) return false;

            bool exists = false;

            for (int i = 0; i < receivers.Count; i++)
            {
                if (receivers[i] == receiver)
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }
    }

    public static class EventRegister
    {
        public delegate void Delegate<T>(T eventType);

        public static void IOCEventStartListening<EventType>(this IOCEventListener<EventType> caller) where EventType : struct
        {
            IOCEventManager.AddListener<EventType>(caller);
        }

        public static void IOCEventStopListening<EventType>(this IOCEventListener<EventType> caller) where EventType : struct
        {
            IOCEventManager.RemoveListener<EventType>(caller);
        }
    }

    public interface IOCEventListenerBase { };

    public interface IOCEventListener<T> : IOCEventListenerBase
    {
        void OnIOCEvent(T eventType);
    }
}
