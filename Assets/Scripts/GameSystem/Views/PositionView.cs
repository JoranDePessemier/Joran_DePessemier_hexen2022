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

        public void Dropped(CardView usedCard)
        {
            _parent.ChildDropped(this,usedCard);
        }

        public void Dragged(CardView usedCard)
        {
            _parent.ChildDragged(this, usedCard);
        }

        public void Activate()
        {
            OnActivate?.Invoke();
        }

        public void DeActivate()
        {
            OnDeActivate?.Invoke();
        }
    }

}
