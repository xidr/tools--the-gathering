using System;
using UnityEngine;

namespace Tools.EventBus {
    public class Hero : MonoBehaviour {
        int health;
        int mana;

        EventBinding<TestEvent> testEventBinding;
        EventBinding<PlayerEvent> playerEventBinding;

        void OnEnable() {
            testEventBinding = new EventBinding<TestEvent>(HandleTestEvent);
            EventBus<TestEvent>.Register(testEventBinding);
            
            playerEventBinding = new EventBinding<PlayerEvent>(HandlePlayerEvent);
            EventBus<PlayerEvent>.Register(playerEventBinding);
        }

        void OnDisable() {
            EventBus<TestEvent>.Deregister(testEventBinding);
            EventBus<PlayerEvent>.Deregister(playerEventBinding);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.A)) {
                EventBus<TestEvent>.Raise(new TestEvent());
            }

            if (Input.GetKeyDown(KeyCode.B)) {
                EventBus<PlayerEvent>.Raise(new PlayerEvent {
                    health = this.health,
                    mana = this.mana,
                }); 
            }
        }

        void HandleTestEvent() {
            Debug.Log("Test event received!");
        }

        void HandlePlayerEvent(PlayerEvent playerEvent) {
            Debug.Log($"Player event received: {playerEvent}");
        }
    }
}