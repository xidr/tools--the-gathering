namespace Tools.Command
{
    public abstract class InAirModuleCommand : ICommand
    {
        protected readonly Stats.InAir _stats;

        protected InAirModuleCommand(Stats.InAir stats)
        {
            _stats = stats;
        }
        
        public abstract void Execute();

        public static T Create<T>(Stats.InAir stats) where T : InAirModuleCommand
        {
            // Dynamically create a new instance of a type
            // Especially useful when the type of object is now known at compile time
            return (T) System.Activator.CreateInstance(typeof(T), stats);
        }
    }

    public class NonParabolicModule : InAirModuleCommand
    {
        public NonParabolicModule(Stats.InAir stats) : base(stats) { }

        public override void Execute()
        {
            
        }
    }
}