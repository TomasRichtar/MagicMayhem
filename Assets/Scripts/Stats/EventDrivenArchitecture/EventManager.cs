using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventDrivenArchitecture
{
    public static class EventManager
    {
        private static Dictionary<string, Action<object>> eventDictionary = new Dictionary<string, Action<object>>();

        public static void Subscribe(string eventName, Action<object> listener)
        {
            if (!eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] = null;
            }
            eventDictionary[eventName] += listener;
        }

        public static void Unsubscribe(string eventName, Action<object> listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] -= listener;
            }
        }

        public static void TriggerEvent(string eventName, object eventData)
        {
            if (eventDictionary.ContainsKey(eventName) && eventDictionary[eventName] != null)
            {
                eventDictionary[eventName].Invoke(eventData);
            }
        }
    }
}
