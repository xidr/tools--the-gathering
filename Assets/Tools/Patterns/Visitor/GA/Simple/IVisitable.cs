namespace Patterns.Visitor.GA.Simple {
    public interface IVisitable {
        void Accept(IVisitor visitor);
    }
}