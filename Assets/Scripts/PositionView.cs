using UnityEngine;
using UnityEngine.EventSystems;

public class PositionView : MonoBehaviour, IPointerClickHandler
{
    public Position CubePosition => PositionHelper.CubePosition(transform.position);

    private BoardView _parent;

    private void Start()
    {
        _parent = GetComponentInParent<BoardView>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _parent.ChildClicked(this);
    }
}
