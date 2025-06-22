using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Patterns.Command.GA {
    
    [Serializable]
    public abstract class HeroCommand : ICommand {
        protected readonly IEntity _hero;

        protected HeroCommand(IEntity hero) {
            _hero = hero;
        }
        
        public abstract Task Execute();
        
        // Static Factory/Creation Method
        public static T Create<T>(IEntity hero) where T : HeroCommand {
            return (T) System.Activator.CreateInstance(typeof(T), hero);
        }
    }
    
    [Serializable]
    public class AttackCommand : HeroCommand {
        public AttackCommand(IEntity hero) : base(hero) {}

        public override async Task Execute() {
            Debug.Log("Attack");
            _hero.Attack();
            await Awaitable.WaitForSecondsAsync(_hero.animations.Attack());
            _hero.animations.Idle();
        }
    }

    [Serializable]
    public class SpinCommand : HeroCommand {
        public SpinCommand(IEntity hero) : base(hero) {}
        
        public override async Task Execute() {
            Debug.Log("Spin");
            _hero.Spin();
            await Awaitable.WaitForSecondsAsync(_hero.animations.Spin());
            _hero.animations.Idle();
        }
    }
}