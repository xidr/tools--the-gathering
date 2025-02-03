namespace Tools.Command
{
    public class Stats
    {
        public interface IXMovement 
        {
            public float acceleration { get; set; }
            public float deceleration { get; set; }
            public float maxSpeed { get; set; }
        }
        
        public interface IGravity
        {
            public float gravity { get; set; }
        }
        
        public interface IXMovementXGravity : IXMovement, IGravity { }
        
        [System.Serializable]
        public class InAir : IXMovementXGravity
        {
            public float acceleration { get; set; } = 3f;
            public float deceleration { get; set; } = 6f;
            public float maxSpeed { get; set; } = 6f;

            public float gravity { get; set; } = 9.81f;
        }
    }
}