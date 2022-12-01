using BoardSystem;
using GameSystem.Helpers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    public class PositionView : MonoBehaviour
    {
        public Position CubePosition => PositionHelper.CubePosition(transform.position);

        private BoardView _parent;

        private void Start()
        {
            _parent = GetComponentInParent<BoardView>();
        }

        public void Dragged()
        {
            _parent.ChildDragged(this);
        }
    }

}
