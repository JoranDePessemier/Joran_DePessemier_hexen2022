using GameSystem.Views;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameStates
{
    class MenuState:State
    {
        private MenuView _menuView;

        public override void OnEnter()
        {
            base.OnEnter();
            AsyncOperation op = SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
            op.completed += InitialiseScene;
        }

        private void InitialiseScene(AsyncOperation obj)
        {
            _menuView = GameObject.FindObjectOfType<MenuView>();
            _menuView.PlayClicked += OnPlayClicked; 

        }

        private void OnPlayClicked(object sender, EventArgs e)
        {
            StateMachine.MoveTo(States.Playing);
        }

        public override void OnExit()
        {
            base.OnExit();

            if(_menuView != null)
            {
                _menuView.PlayClicked -= OnPlayClicked;
            }

            SceneManager.UnloadSceneAsync("Menu");
        }
    }
}
