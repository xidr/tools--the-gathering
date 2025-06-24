using Patterns.IoS_ServiceLocator.GA;
using UnityEngine;

namespace Patterns.Mediator.GA {
    public class AgentMediator : Mediator<Agent> {
        void Awake() {
            ServiceLocator.global.Register(this as Mediator<Agent>);
        }
        
        protected override bool MediatorConditionMet(Agent target) => target.status == AgentStatus.Active;

        protected override void OnRegistered(Agent entity) {
            Debug.Log("AgentMediator.OnRegistered");
            Broadcast(entity, new MessagePayload.Builder(entity).WithContent("Registered").Build());
        }

        protected override void OnDeregistered(Agent entity) {
            Debug.Log("AgentMediator.OnDeregistered");
            Broadcast(entity, new MessagePayload.Builder(entity).WithContent("Deregistered").Build());
        }

    }
}