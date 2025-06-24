using System;
using Patterns.IoS_ServiceLocator.GA;
using UnityEngine;

namespace Patterns.Mediator.GA.LawOfDemeter {
    public class Hero : MonoBehaviour {
        // public HeroStats stats { get; private set; }
        // public HeroHealth health { get; private set; }
        // Anytime a class accesses the Heros' components it violates the Law of Demeter
        // ie: hero.stats.AdjustStats(10);

        HeroStats _stats;
        HeroHealth _health;
        HeroMediator _mediator;

        void Awake() {
            _stats = new HeroStats();
            _health = new HeroHealth();
            ServiceLocator.global.Register(_mediator = new HeroMediator(_stats, _health));
        }
    }

    public class HeroStats {
        int _currentStats;

        public void AdjustStats(int changeAmount) {
            _currentStats += changeAmount;
        }
    }

    public class HeroHealth {
        int _health;
        
        public void Heal(int healAmount) {
            _health += healAmount;
        }

        public void TakeDamage(int damageAmount) {
            _health -= damageAmount;
        }
    }
}