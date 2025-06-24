namespace Patterns.Mediator.GA.LawOfDemeter {
    public class HeroMediator {
        HeroStats _stats;
        HeroHealth _health;

        public HeroMediator(HeroStats stats, HeroHealth health) {
            _stats = stats;
            _health = health;
        }

        public void ChangeStats(int changeAmount) {
            _stats.AdjustStats(changeAmount);
        }

        public void Heal(int healAmount) {
            _health.Heal(healAmount);
        }

        public void Damage(int damageAmount) {
            _health.TakeDamage(damageAmount);
        }
    }
}