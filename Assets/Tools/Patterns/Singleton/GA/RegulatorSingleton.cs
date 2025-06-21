using UnityEngine;

namespace Patterns.Singleton.GA {
    public class RegulatorSingleton<T> : MonoBehaviour where T : Component {
        protected static T _instance;

        public static bool HasInstance => _instance != null;

        public static T Instance {
            get {
                if (_instance == null) {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null) {
                        var go = new GameObject(typeof(T).Name + " Auto-Generated");
                        go.hideFlags = HideFlags.HideAndDontSave;
                        _instance = go.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        public float InitializationTime { get; private set; }

        /// <summary>
        /// Make sure to call base.Awake() in override if you need awake.
        /// </summary>
        protected virtual void Awake() {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton() {
            if (!Application.isPlaying) return;
            InitializationTime = Time.time;
            DontDestroyOnLoad(_instance);

            T[] oldInstances = FindObjectsByType<T>(FindObjectsSortMode.None);
            foreach (T old in oldInstances) {
                if (old.GetComponent<RegulatorSingleton<T>>().InitializationTime < InitializationTime) {
                    Destroy(old.gameObject);
                }
            }

            if (_instance == null) {
                _instance = this as T;
            }
        }
    }
}