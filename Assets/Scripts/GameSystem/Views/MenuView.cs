using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{
    class MenuView : MonoBehaviour
    {
        public event EventHandler<EventArgs> PlayClicked;

        public void Play()
        {
            OnPlayClicked(EventArgs.Empty);
        }

        private void OnPlayClicked(EventArgs eventArgs)
        {
            EventHandler<EventArgs> handler = PlayClicked;
            handler?.Invoke(this, eventArgs);
        }
    }
}
