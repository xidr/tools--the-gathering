namespace Patterns.Visitor.GA.Reflective {
    public interface IVisitable {
        void Accept(IVisitor visitor);
    }
}