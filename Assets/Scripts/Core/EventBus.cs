using System;
using System.Collections.Generic;
using UnityEngine;

namespace MountAndBlade2D.Core
{
    /// <summary>
    /// Lightweight pub/sub bus to decouple world, UI, and combat systems.
    /// </summary>
    public class EventBus : MonoBehaviour
    {
        private readonly Dictionary<Type, object> _subjects = new();

        public EventSubject<TEvent> GetSubject<TEvent>() where TEvent : struct
        {
            var type = typeof(TEvent);
            if (_subjects.TryGetValue(type, out var existing))
            {
                return (EventSubject<TEvent>)existing;
            }

            var subject = new EventSubject<TEvent>();
            _subjects[type] = subject;
            return subject;
        }
    }

    public class EventSubject<TEvent> where TEvent : struct
    {
        private event Action<TEvent> Handlers;

        public void Publish(TEvent payload)
        {
            Handlers?.Invoke(payload);
        }

        public void Subscribe(Action<TEvent> callback)
        {
            Handlers += callback;
        }

        public void Unsubscribe(Action<TEvent> callback)
        {
            Handlers -= callback;
        }
    }
}
