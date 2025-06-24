using System;
using System.Collections.Generic;
using Alchemy.Inspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Patterns.IoS_ServiceLocator.GA {
    public class HeroSL : MonoBehaviour {
        [Title("Registered Services")] [SerializeField]
        List<Object> _services;

        void Awake() {
            ServiceLocator sl = ServiceLocator.For(this);
            foreach (Object service in _services) {
                sl.Register(service.GetType(), service);
            }
        }
    }
}