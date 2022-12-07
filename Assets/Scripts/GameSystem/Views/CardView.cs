using CardSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSystem.Views
{
    public class CardView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField]
        private Canvas _canvas;

        [SerializeField]
        private LayerMask _dropMask;

        [SerializeField]
        private CardType _type;
        public CardType Type => _type;


        private GameObject _draggingIcon;
        private RectTransform _draggingPlane;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_canvas) return;

            CreateIcon();

            SetDraggedPosition(eventData);
        }

        private void SetDraggedPosition(PointerEventData data)
        {
            if (data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
                _draggingPlane = data.pointerEnter.transform as RectTransform;

            RectTransform iconTransform = _draggingIcon.GetComponent<RectTransform>();
            Vector3 mousePosition;
            if (_draggingPlane != null && RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggingPlane, data.position, data.pressEventCamera, out mousePosition))
            {
                iconTransform.position = mousePosition;
                iconTransform.rotation = _draggingPlane.rotation;
            }
            else
            {
                iconTransform.position = this.GetComponent<RectTransform>().position;
            }
        }

        private void CreateIcon()
        {
            _draggingIcon = new GameObject("icon");

            _draggingIcon.transform.SetParent(_canvas.transform, false);
            _draggingIcon.transform.SetAsLastSibling();

            Image image = _draggingIcon.AddComponent<Image>();

            image.sprite = this.GetComponentInChildren<Image>().sprite;
            image.SetNativeSize();

            RectTransform iconTransform = image.rectTransform;
            iconTransform.localScale = this.GetComponent<RectTransform>().localScale;
            iconTransform.localPosition = this.GetComponent<RectTransform>().localPosition;

            image.raycastTarget = false;

        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_draggingIcon!=null)
            {
                SetDraggedPosition(eventData);

                GameObject rayObject = eventData.pointerCurrentRaycast.gameObject;

                if (rayObject && (_dropMask & (1 << rayObject.layer)) != 0)
                {
                    rayObject.GetComponentInParent<PositionView>().Dragged(this);
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_draggingIcon)
            {
                Destroy(_draggingIcon);


                GameObject rayObject = eventData.pointerCurrentRaycast.gameObject;

                if (rayObject && (_dropMask & (1 << rayObject.layer)) != 0)
                {
                    Destroy(this.gameObject);

                    rayObject.GetComponentInParent<PositionView>().Dropped(this);
                }
            }
        }
    }

}