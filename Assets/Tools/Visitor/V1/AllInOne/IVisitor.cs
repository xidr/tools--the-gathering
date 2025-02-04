namespace Tools.Visitor.V1.AllInOne
{
    public interface IVisitor
    {
        void Visit(object o);
        void Visit(HealthComponent healthComponent);
        void Visit(ManaComponent manaComponent);
    }
}