using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class PositionView : MonoBehaviour, IPointerClickHandler
{
    public event EventHandler Clicked;

    public Position CubePosition => PositionHelper.CubePosition(transform.position);

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked(EventArgs.Empty);
        transform.position = PositionHelper.WorldPosition(CubePosition);
        Debug.Log(CubePosition.ToString());
    }

    protected virtual void OnClicked(EventArgs eventArgs)
    {
        EventHandler handler = Clicked;
        handler?.Invoke(this, eventArgs);
    }
}
