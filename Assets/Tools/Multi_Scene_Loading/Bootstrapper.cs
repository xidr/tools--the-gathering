using Tools.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools.Multi_Scene_Loading {
    public class Bootstrapper : PersistentSingleton<Bootstrapper> {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static async void Init() {
            Debug.Log("Bootstrapper init");
            await SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Single);
        }
    }
}