namespace Tools.Visitor.V1.AllInOne
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}