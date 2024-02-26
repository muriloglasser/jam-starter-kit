using System;
using System.Collections.Generic;

namespace EntityCreator
{
    /// <summary>
    /// A class that facilitates event dispatching, registration, and unregistration for various event types.
    /// </summary>
    public class EventDispatcher
    {
        // A dictionary to store observers for various event types, where the key is a hash of the event type and the value is a list of observers.
        private static Dictionary<int, List<object>> m_observers;

        static EventDispatcher()
        {
            m_observers = new Dictionary<int, List<object>>();
        }

        /// <summary>
        /// Dispatches an event with generic data to all registered observers.
        /// </summary>
        /// <typeparam name="T">The generic type of data to dispatch.</typeparam>
        /// <param name="eventData">The generic data to dispatch.</param>
        public static void Dispatch<T>(object eventData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Registers an observer to listen for events of a specific generic type.
        /// </summary>
        /// <typeparam name="T">The generic type of event data to observe.</typeparam>
        /// <param name="callback">The callback function to execute when the event is dispatched.</param>
        public static void RegisterObserver<T>(Action<T> callback) where T : struct
        {
            List<object> observerList;

            var eventTypeHash = GetEventTypeHash<T>();

            if (!m_observers.TryGetValue(eventTypeHash, out observerList))
                observerList = new List<object>();

            observerList.Add(callback);

            m_observers[eventTypeHash] = observerList;
        }

        /// <summary>
        /// Unregisters an observer from listening to events of a specific generic type.
        /// </summary>
        /// <typeparam name="T">The generic type of event data to unregister from.</typeparam>
        /// <param name="callback">The callback function to unregister.</param>
        public static void UnregisterObserver<T>(Action<T> callback) where T : struct
        {
            var eventTypeHash = GetEventTypeHash<T>();

            if (m_observers.ContainsKey(eventTypeHash))
            {
                List<object> observerList;

                if (m_observers.TryGetValue(eventTypeHash, out observerList))
                {
                    observerList.Remove(callback);
                    if (observerList.Count == 0)
                        m_observers.Remove(eventTypeHash);
                }
            }
        }

        /// <summary>
        /// Dispatches an event with specific data of a generic type to all registered observers.
        /// </summary>
        /// <typeparam name="T">The generic type of data to dispatch.</typeparam>
        /// <param name="eventData">The specific data of the generic type to dispatch.</param>
        public static void Dispatch<T>(T eventData) where T : struct
        {
            List<object> observerList;

            if (!m_observers.TryGetValue(GetEventTypeHash<T>(), out observerList))
                observerList = new List<object>();

            for (int i = 0; i < observerList.Count; i++)
            {
                ((Action<T>)observerList[i])?.Invoke(eventData);
            }
        }

        /// <summary>
        /// Private method to get a hash code for a generic event type.
        /// </summary>
        /// <typeparam name="T">The generic type for which to calculate the hash code.</typeparam>
        /// <returns>The hash code for the generic type.</returns>
        private static int GetEventTypeHash<T>()
        {
            return typeof(T).GetHashCode();
        }
    }
}
