using System.ComponentModel;
using Tools.Extension_Methods;
using UnityEngine;

namespace Patterns.IoS_ServiceLocator.GA {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ServiceLocator))]
    public abstract class Bootstrapper : MonoBehaviour {
        ServiceLocator _container;
        internal ServiceLocator container => _container.OrNull() ?? (_container = GetComponent<ServiceLocator>());

        bool _hasBeenBootstrapped;

        void Awake() => BootstrapOnDemand();

        public void BootstrapOnDemand() {
            if (_hasBeenBootstrapped) return;
            _hasBeenBootstrapped = true;
            Bootstrap();
        }
        
        protected abstract void Bootstrap();
    }

    [AddComponentMenu("Patterns/IoS_ServiceLocator.GA/ServiceLocator Global")]
    public class ServiceLocatorGlobalBootstrapper : Bootstrapper {
        [SerializeField] bool _dontDestroyOnLoad = true;
        
        protected override void Bootstrap() {
            container.ConfigureAsGlobal(_dontDestroyOnLoad);
        }
    }
    
    [AddComponentMenu("Patterns/IoS_ServiceLocator.GA/ServiceLocator Scene")]
    public class ServiceLocatorSceneBootstrapper : Bootstrapper {
        protected override void Bootstrap() {
            container.ConfigureForScene();
        }
    }
}