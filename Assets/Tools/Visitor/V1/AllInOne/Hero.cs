using System.Collections.Generic;
using Tools.Extension_Methods_and_Utils.GetOrAdd;
using UnityEngine;

namespace Tools.Visitor.V1.AllInOne
{
    public class Hero : MonoBehaviour, IVisitable
    {
        private List<IVisitable> _visitableComponents = new List<IVisitable>();

        private void Start()
        {
            _visitableComponents.Add(gameObject.GetOrAdd<HealthComponent>());
            _visitableComponents.Add(gameObject.GetOrAdd<ManaComponent>());
        }
        
        public void Accept(IVisitor visitor)
        {
            foreach (var visitableComponent in _visitableComponents)
            {
                visitableComponent.Accept(visitor);
            }
        }
    }
}