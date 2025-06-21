using Tools.Extension_Methods;
using UnityEngine;

namespace Patterns.Strategy.GA {
    public class ProjectileBuilder {
        GameObject _projectilePrefab;
        float _speed;
        float _duration;

        public ProjectileBuilder WithProjectilePrefab(GameObject projectilePrefab) {
            _projectilePrefab = projectilePrefab;
            return this;
        }

        public ProjectileBuilder WithSpeed(float speed) {
            _speed = speed;
            return this;
        }

        public ProjectileBuilder WithDuration(float duration) {
            _duration = duration;
            return this;
        }

        public GameObject Build(Transform origin) {
            var instantiatePosition = origin.position + origin.forward * 2f;

            // Why he can and I CANNOT use it without inheriting monobeh class?
            GameObject fireball = Object.Instantiate(_projectilePrefab, instantiatePosition.With(y: 1f), Quaternion.identity);
            // Should I do it like that?
            
            
            // Fire it FIRE
            return fireball;
        }
    }
}