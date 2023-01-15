using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.GameStates
{
    public class StateMachine : MonoBehaviour
    {
        private Dictionary<string, State> _states = new Dictionary<string, State>();
        
        private Stack<string> _currentStateNames = new Stack<string>();

        public string InitialState
        {
            set
            {
                _currentStateNames.Push(value);
                CurrentState.OnEnter();
                CurrentState.OnResume();
            }
        }

        public State CurrentState => _states[_currentStateNames.Peek()];

        public void MoveTo(string stateName)
        {
            CurrentState.OnSuspend();
            CurrentState.OnExit();

            _currentStateNames.Pop();
            _currentStateNames.Push(stateName);

            CurrentState.OnEnter();
            CurrentState.OnResume();
        }

        public void Push(string stateName)
        {
            CurrentState.OnSuspend();

            _currentStateNames.Push(stateName);

            CurrentState.OnEnter();
            CurrentState.OnResume();
        }

        public void Pop(string stateName)
        {
            CurrentState.OnSuspend();
            CurrentState.OnExit();

            _currentStateNames.Pop();

            CurrentState.OnResume();
        }
    }
}


