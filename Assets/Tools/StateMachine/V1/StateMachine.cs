using System.Collections.Generic;
using UnityEngine;

namespace Tools.StateMachine.V1
{
    public class StateMachine : MonoBehaviour
    {
        private  List<IState> _states = new();
        private IState _currentState;
        public IState currentState => _currentState;
        

        public void Init(MonoBehaviour core)
        {
            GetComponentsInChildren(_states);
            _states.ForEach(x =>
            {
                x.Init(core);
                x.OnTransitionRequired += ChangeState;
            });
            ChangeState(_states[0].GetType());
        }
        
        public void OnDestroy()
        {
            _states.ForEach(x =>
            {
                x.OnTransitionRequired -= ChangeState;
            });
        }

        public void ChangeState(System.Type nextStateType)
        {
            var nextState = _states.Find(x =>
            {
                return x.GetType() == nextStateType;
            });
            
            if (nextState != null && !Equals(_currentState, nextState))
            {
                if (_currentState != null)
                {
                    _currentState.Exit();
                }
                nextState.Enter();
                _currentState = nextState;
            }
        }
    }
}