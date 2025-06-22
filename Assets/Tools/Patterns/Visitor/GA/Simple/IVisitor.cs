namespace Patterns.Visitor.GA.Simple {
    public interface IVisitor {
        void Visit(HealthComponent healthComponent);
        void Visit(ManaComponent manaComponent);
    }

}