using System;

namespace Tools.EventBus {
    // He used an interface because he utilizes Explicit Interface Feature
    internal interface IEventBinding<T> {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }
    
    public class EventBinding<T> : IEventBinding<T> where T : IEvent {
        // Here is a longer version
        // private Action<T> OnEvent = delegate(T _) { }; // And it's the old-school way (before lambdas)
        private Action<T> OnEvent = _ => { }; // It's lambda function?
        private Action OnEventNoArgs = () => { };

        // Omg THIS is here an Explicit Interface Implementation
        // It only appears when I use the var though the interface
        // That's wild
        Action<T> IEventBinding<T>.OnEvent {
            get => OnEvent;
            set => OnEvent = value;
        }

        Action IEventBinding<T>.OnEventNoArgs {
            get => OnEventNoArgs;
            set => OnEventNoArgs = value;
        }

        // And two constructors here
        // And this way of doing so is called an expression body
        public EventBinding(Action<T> onEvent) => this.OnEvent = onEvent;
        // Is the same as
        // public EventBinding(Action<T> onEvent) {
        //     this.OnEvent = onEvent;
        // }
        public EventBinding(Action onEventNoArgs) => this.OnEventNoArgs = onEventNoArgs;
        
        // And those here are body expressions as well
        public void Add(Action onEvent) => OnEventNoArgs += onEvent;
        public void Remove(Action onEvent) => OnEventNoArgs -= onEvent;
        
        public void Add(Action<T> onEvent) => OnEvent += onEvent;
        public void Remove(Action<T> onEvent) => OnEvent -= onEvent;
    }
}