using System.Collections.Generic;
using Tools.Extension_Methods;
using UnityEngine;

namespace Patterns.Visitor.GA.Reflective {
    public class Hero: MonoBehaviour {
        List<IVisitable> _visitableComponents = new();

        void Start() {
            _visitableComponents.Add(gameObject.GetOrAdd<HealthComponent>());
            _visitableComponents.Add(gameObject.GetOrAdd<ManaComponent>());
        }

        public void Accept(IVisitor visitor) {
            foreach (var component in _visitableComponents) {
                component.Accept(visitor);
            }
        }
    }
}