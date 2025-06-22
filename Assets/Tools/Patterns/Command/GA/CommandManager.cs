using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Patterns.Command.GA {
    public class CommandManager : MonoBehaviour {
        public IEntity entity;
        [SerializeReference] public ICommand singleCommand;
        [SerializeReference] public List<ICommand> commands;

        readonly CommandInvoker _commandInvoker = new CommandInvoker();
        bool _isCommandExecuting;
        
        void Start() {
            entity = GetComponent<IEntity>();

            singleCommand = HeroCommand.Create<AttackCommand>(entity);

            commands = new List<ICommand>() {
                HeroCommand.Create<AttackCommand>(entity), HeroCommand.Create<SpinCommand>(entity)
            };
        }

        async Task ExecuteCommands(List<ICommand> commands) {
            _isCommandExecuting = true;
            await _commandInvoker.ExecuteCommand(commands);
            _isCommandExecuting = false;
        }

        void Update() {
            if (_isCommandExecuting) return;
            
            if (Input.GetKeyDown(KeyCode.Space)) {
                ExecuteCommands(commands);
            }

            if (Input.GetKeyDown(KeyCode.R)) {
                ExecuteCommands(new List<ICommand>() { singleCommand });
            }
        }
    }

    public class CommandInvoker {
        public async Task ExecuteCommand(List<ICommand> commands) {
            foreach (ICommand command in commands) {
                await command.Execute();
            }
        }
    }
}