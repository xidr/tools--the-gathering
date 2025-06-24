using UnityEngine;

namespace Patterns.Mediator.GA {
    // This is the message class
    public abstract class Payload<TData> : IVisitor {
        public abstract TData content { get; set; }
        public abstract void Visit<T>(T visitable) where T : Component, IVisitable;
    }

    public class MessagePayload : Payload<string> {
        public Agent source { get; set; } 
        public override string content { get; set; }
        
        // To enforce construction through the Builder
        public MessagePayload() {}
        
        public override void Visit<T>(T visitable) {
            Debug.Log(visitable.name);
        }
        
        public class Builder {
            MessagePayload _payload = new MessagePayload();
            
            public Builder(Agent source) => _payload.source = source;

            public Builder WithContent(string content) {
                _payload.content = content;
                return this;
            }
            
            public MessagePayload Build() => _payload;
        }
    }


}