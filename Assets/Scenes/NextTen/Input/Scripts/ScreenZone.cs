using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Easybalo.NextTen.Input {
    public class ScreenZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
        private Image image;
        public bool Active { get; private set; }

        private void Start () {
            image = GetComponent<Image> ();
            Active = false;
        }

        public void OnPointerDown (PointerEventData eventData) {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle (image.rectTransform,
                eventData.position, eventData.pressEventCamera, out pos)) {
                Active = true;
            }
        }

        public void OnPointerUp (PointerEventData eventData) {
            Active = false;
        }
    }
}
