using UnityEngine;

namespace Patterns.Strategy.GA {
    public class ProjectileSpawnerStrategy : SpellStrategy {
        public GameObject projectilePrefab;
        public float speed = 10f;

        public override void CastSpell(Transform origin) {
            new ProjectileBuilder()
                .WithProjectilePrefab(projectilePrefab)
                .WithSpeed(speed)
                .Build(origin);
        }
    }
}