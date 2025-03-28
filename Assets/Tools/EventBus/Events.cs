namespace Tools.EventBus {
    public interface IEvent {
    }
    
    // And he uses struct bcs:
    // "Structs are allocated on a stack, not a heap so they put way less pressure on the garbage collector"
    // Pretty cool
    public struct TestEvent : IEvent {}

    public struct PlayerEvent : IEvent {
        public int health;
        public int mana;
    }
}