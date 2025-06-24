using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Patterns.IoS_ServiceLocator.GA {
    // We keep registered services here, actually
    public class ServiceManager {
        readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        public IEnumerable<object> registeredServices => _services.Values;

        public bool TryGet<T>(out T service) where T : class {
            Type type = typeof(T);
            
            if (_services.TryGetValue(type, out object obj)) {
                service = obj as T;
                return true;
            }
            
            service = null;
            
            return false;
        }
        
        public T Get<T>() where T : class {
            Type type = typeof(T);

            if (_services.TryGetValue(type, out object obj)) {
                return obj as T;
            }

            // Forgot to register the service we are looking for
            throw new ArgumentException("Not found");
        }

        public ServiceManager Register<T>(T service) {
            Type type = typeof(T);

            if (_services.TryAdd(type, service)) {
                Debug.LogError("Service already registered: " + type);
            }
            
            return this;
        }

        // The same as above BUT:
        // For the case when a service implements several types
        // so I want to specify which type it is
        public ServiceManager Register(Type type, object service) {
            if (!type.IsInstanceOfType(service)) {
                throw new ArgumentException(type + " is not an instance of " + service.GetType());
            }
            
            if (_services.TryAdd(type, service)) {
                Debug.LogError("Service already registered: " + type);
            }
            
            return this;
        }
    }
}