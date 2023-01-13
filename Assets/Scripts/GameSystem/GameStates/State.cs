using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.GameStates
{
    public abstract class State 
    {
        public StateMachine StateMachine { get; set; }

        public virtual void OnExit() { }


        public virtual void OnEnter() { }

        public virtual void OnSuspend() { }


        public virtual void OnResume() { }
    }
}

