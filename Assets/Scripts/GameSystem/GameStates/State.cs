using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.GameStates
{
    public abstract class State
    {
        public StateMachine StateMachine { get; set; }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        
        public virtual void OnResume() { }
        public virtual void OnSuspend() { }

    }
}
