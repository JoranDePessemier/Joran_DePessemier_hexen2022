using BoardSystem;
using GameSystem.Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    public class PositionView : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent OnActivate;

        [SerializeField]
        private UnityEvent OnDeActivate;

        public Position CubePosition => PositionHelper.CubePosition(transform.position);

        private BoardView _parent;

        private void Start()
        {
            _parent = GetComponentInParent<BoardView>();
        }

        //when a card calls the dropped method, send the data over to the boardview class where it will be processed
        public void Dropped(CardView usedCard)
        {
            _parent.ChildDropped(this,usedCard);
        }

        //when a card calls the dragged method, send the data over to the boardview class where it will be processed
        public void Dragged(CardView usedCard)
        {
            _parent.ChildDragged(this, usedCard);
        }

        //used when the tile is in a card moveset
        public void Activate()
        {
            OnActivate?.Invoke();
        }

        //used when the tile is not in a card moveset
        public void DeActivate()
        {
            OnDeActivate?.Invoke();
        }
    }

}
