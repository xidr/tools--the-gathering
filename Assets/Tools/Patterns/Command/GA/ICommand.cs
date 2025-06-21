using System.Threading.Tasks;
using UnityEngine;

namespace Patterns.Command.GA {
    public interface ICommand {
        Task Execute();
    }
}