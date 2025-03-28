using System;
using System.Collections.Generic;
using UnityEngine;


// Find any event type for assemblies and create a bus for them

namespace Tools.EventBus {
    public static class EventBus<T> where T : IEvent {
        static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

        public static void Register(EventBinding<T> binding) => bindings.Add(binding);
        public static void Deregister(EventBinding<T> binding) => bindings.Remove(binding);

        // @ is pronounced as "at"
        // But why does he call both events?
        public static void Raise(T @event) {
            foreach (var binding in bindings) {
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
        }
        
        static void Clear() {
            Debug.Log($"Clearing {typeof(T).Name} bindings");
            bindings.Clear();
        }
    }
    
}