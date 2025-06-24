using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityUtils;

namespace Patterns.Mediator.GA {
    public abstract class Mediator<T> : MonoBehaviour where T : Component, IVisitable {
        readonly List<T> _entities = new List<T>();

        public void Register(T entity) {
            if (!_entities.Contains(entity)) {
                _entities.Add(entity);
                OnRegistered(entity);
            }
        }

        protected virtual void OnRegistered(T entity) {
            // noop
        }

        public void Deregister(T entity) {
            if (_entities.Contains(entity)) {
                _entities.Remove(entity);
                OnDeregistered(entity);
            }
        }

        protected virtual void OnDeregistered(T entity) {
            // noop
        }

        // Visitor bcs different types of payloads that are being delivered
        
        // To a single entity 
        public void Message(T source, T target, IVisitor message) {
            _entities.FirstOrDefault(entity => entity.Equals(target))?.Accept(message);
        }

        // To multiple entities based on some condition (predicate)
        public void Broadcast(T source, IVisitor message, Func<T, bool> predicate = null) {
            _entities.Where(target => source != target && SenderConditionMet(target, predicate) && MediatorConditionMet(target))
                .ForEach(target => target.Accept(message));
        }
        
        bool SenderConditionMet(T target, Func<T, bool> predicate) => predicate == null || predicate(target);
        
        protected abstract bool MediatorConditionMet(T target);
    }
}