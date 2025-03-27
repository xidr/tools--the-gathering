using UnityEngine;

namespace Tools.Singleton {
    public class PersistentSingleton<T> : MonoBehaviour where T : Component {
        protected static T _instance;

        public static bool HasInstance => _instance != null;
        public static T TryGetInstance() => HasInstance ? _instance : null;

        public static T Instance {
            get {
                if (_instance == null) {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null) {
                        var go = new GameObject(typeof(T).Name + " Auto-Generated");
                        _instance = go.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        [SerializeField] private bool _autoUnparentOnAwake = true;

        /// <summary>
        /// Make sure to call base.Awake() in override if you need awake.
        /// </summary>
        protected virtual void Awake() {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton() {
            if (!Application.isPlaying) return;

            if (_autoUnparentOnAwake) {
                transform.SetParent(null);
            }

            if (_instance == null) {
                _instance = this as T;
                DontDestroyOnLoad(_instance);
            }
            else {
                Destroy(gameObject);
            }
        }
    }
}