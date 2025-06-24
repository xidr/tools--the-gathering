using System;
using System.Collections.Generic;
using System.Linq;
using Tools.Extension_Methods;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Patterns.IoS_ServiceLocator.GA {
    public class ServiceLocator : MonoBehaviour {
        static ServiceLocator _global;
        static Dictionary<Scene, ServiceLocator> _sceneContainers;

        const string GLOBAL_SERVICE_LOCATOR_NAME = "Service Locator [Global]";
        const string SCENE_SERVICE_LOCATOR_NAME = "Service Locator [Scene]";

        readonly ServiceManager _services = new();

        internal void ConfigureAsGlobal(bool dontDestroyOnLoad) {
            if (_global == this) {
                Debug.LogWarning("Global is already configured");
            }
            else if (_global != null) {
                Debug.LogError("Another Global is already configured");
            }
            else {
                _global = this;
                if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
            }
        }

        internal void ConfigureForScene() {
            Scene scene = gameObject.scene;

            if (_sceneContainers.ContainsKey(scene)) {
                Debug.LogError("Scene already configured");
                return;
            }

            _sceneContainers.Add(scene, this);
        }

        public static ServiceLocator global {
            get {
                if (_global != null) return _global;

                //bootstrap or initialize the new instance of global as necessary
                if (FindFirstObjectByType<ServiceLocatorGlobalBootstrapper>() is { } found) {
                    found.BootstrapOnDemand();
                    return _global;
                }

                var container = new GameObject(GLOBAL_SERVICE_LOCATOR_NAME, typeof(ServiceLocator));
                container.AddComponent<ServiceLocatorGlobalBootstrapper>().BootstrapOnDemand();

                return _global;
            }
        }

        static List<GameObject> _tmpSceneGameObjects;

        // Ok, here we are just getting ServiceLocator of that scene, right? Not the exact service
        public static ServiceLocator ForSceneOf(MonoBehaviour mb) {
            Scene scene = mb.gameObject.scene;

            if (_sceneContainers.TryGetValue(scene, out ServiceLocator container) && container != mb) {
                return container;
            }

            _tmpSceneGameObjects.Clear();
            scene.GetRootGameObjects(_tmpSceneGameObjects);

            foreach (GameObject go in _tmpSceneGameObjects.Where(go =>
                         go.GetComponent<ServiceLocatorSceneBootstrapper>() != null)) {
                if (go.TryGetComponent(out ServiceLocatorSceneBootstrapper bootstrapper) &&
                    bootstrapper.container != mb) {
                    bootstrapper.BootstrapOnDemand();
                    return bootstrapper.container;
                }
            }

            // If we weren't able to find any scene level ServiceLocator
            return global;
        }

        // When it's right on the actual object we are looking for
        public static ServiceLocator For(MonoBehaviour mb) {
            return mb.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(mb) ?? global;
        }

        public ServiceLocator Register<T>(T service) {
            _services.Register(service);
            return this;
        }

        public ServiceLocator Register(Type type, object service) {
            _services.Register(type, service);
            return this;
        }

        public ServiceLocator Get<T>(out T service) where T : class {
            if (TryGetService(out service)) return this;

            if (TryGetNextInHierarchy(out ServiceLocator container)) {
                container.Get(out service);
                return this;
            }

            throw new ArgumentException("Service not found");
        }

        bool TryGetService<T>(out T service) where T : class {
            return _services.TryGet(out service);
        }

        bool TryGetNextInHierarchy(out ServiceLocator container) {
            if (this == global) {
                container = global;
                return false;
            }

            container = transform.parent.OrNull()?.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(this);
            return container != null;
        }

        void OnDestroy() {
            if (this == _global) {
                _global = null;
            } else if (_sceneContainers.ContainsValue(this)) {
                _sceneContainers.Remove(gameObject.scene);
            }
        }

        // Called before the first scene load
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetStatics() {
            _global = null;
            _sceneContainers = new Dictionary<Scene, ServiceLocator>();
            _tmpSceneGameObjects = new List<GameObject>();
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/ServiceLocator/Add Global")]
        static void AddGlobal() {
            var go = new GameObject(GLOBAL_SERVICE_LOCATOR_NAME, typeof(ServiceLocatorGlobalBootstrapper));
        }

        [MenuItem("GameObject/ServiceLocator/Add Scene")]
        static void AddScene() {
            var go = new GameObject(SCENE_SERVICE_LOCATOR_NAME, typeof(ServiceLocatorSceneBootstrapper));
        }
#endif
    }
}