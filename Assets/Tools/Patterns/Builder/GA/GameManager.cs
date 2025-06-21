using UnityEngine;

namespace Patterns.Builder.GA {
    public class GameManager : MonoBehaviour {

        void Start() {
            Enemy enemy = new Enemy.Builder()
                .WithName("Goblin")
                .WithHealth(100)
                .WithSpeed(5f)
                .WithDamage(10)
                .Build();

            Instantiate(enemy);
            // or put in object pool, etc.
        }
        
    }
}