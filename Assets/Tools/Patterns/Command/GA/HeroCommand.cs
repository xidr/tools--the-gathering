using System.Threading.Tasks;
using UnityEngine;

namespace Patterns.Command.GA {
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
    
    public class AttackCommand : HeroCommand {
        public AttackCommand(IEntity hero) : base(hero) {}

        public override async Task Execute() {
            _hero.Attack();
            await Awaitable.WaitForSecondsAsync(_hero.animations.Attack());
            _hero.animations.Idle();
        }
    }

    public class SpinCommand : HeroCommand {
        public SpinCommand(IEntity hero) : base(hero) {}
        
        public override async Task Execute() {
            _hero.Spin();
            await Awaitable.WaitForSecondsAsync(_hero.animations.Spin());
            _hero.animations.Idle();
        }
    }
}