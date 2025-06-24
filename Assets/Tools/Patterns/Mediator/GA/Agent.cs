using System;
using Patterns.IoS_ServiceLocator.GA;
using UnityEngine;

namespace Patterns.Mediator.GA {
    public class Agent : MonoBehaviour, IVisitable {

        Mediator<Agent> _mediator;

        void Start() {
            ServiceLocator.For(this).Get(out _mediator);
            
            _mediator.Register(this);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.A)) {
                Send(new MessagePayload.Builder(this).WithContent("Hello").Build(), IsNearby);
            }
        }

        void OnDestroy() => _mediator.Deregister(this);

        public AgentStatus status { get; set; } = AgentStatus.Active; 

        public void Accept(IVisitor message) => message.Visit(this);
        
        void Send(IVisitor message) => _mediator.Broadcast(this, message);
        void Send(IVisitor message, Func<Agent, bool> predicate) => _mediator.Broadcast(this, message, predicate);
        
        const float RADIUS = 5f;
        Func<Agent, bool> IsNearby => target => Vector3.Distance(transform.position, target.transform.position) <= RADIUS;
    }

    public enum AgentStatus {
        Active,
        Rest
    }
}