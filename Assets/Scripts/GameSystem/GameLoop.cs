using BoardSystem;
using CardSystem;
using CardSystem.MoveSets;
using GameSystem.GameStates;
using GameSystem.Helpers;
using GameSystem.Views;
using HandFactory;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {


        private StateMachine _stateMachine;

        private void Start()
        {
            _stateMachine = new StateMachine();
            _stateMachine.Register(States.Menu, new MenuState());
            _stateMachine.Register(States.Playing, new PlayingState());

            _stateMachine.InitialState = States.Playing;
        }

    }
}
