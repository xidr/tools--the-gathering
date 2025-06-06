using UnityEngine;

namespace Tools.Strategy.git_amend {
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