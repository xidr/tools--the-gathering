using System.Collections.Generic;
using UnityEngine;

namespace Tools.Tricks.Null_Object_Pattern_and_InterfaceMethods {
    public interface ICommand {
        public void Execute() {
            Debug.Log("Command executed" + GetType().Name);
        }
        
        public static ICommand Null { get; } = new NullCommand();
        class NullCommand : ICommand {
            public void Execute() {
                Debug.Log("Doing NOTHING!");
            }
        }

        public static T Create<T>() where T : ICommand, new() {
            return new T();
        }
    }
    
    public class SpellCommand : ICommand {}
    public class ItemCommand : ICommand {}

    public class Hero : MonoBehaviour {
        private List<ICommand> _commands = new List<ICommand>();

        void Awake() {
            _commands.Add(ICommand.Create<SpellCommand>());
            _commands.Add(ICommand.Create<ItemCommand>());
        }
    }
}