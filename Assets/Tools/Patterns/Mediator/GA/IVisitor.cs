using UnityEngine;

namespace Patterns.Mediator.GA {
    public interface IVisitor {
        void Visit<T>(T visitable) where T : Component, IVisitable;
    }

    public interface IVisitable {
        void Accept(IVisitor visitor);
    }
}