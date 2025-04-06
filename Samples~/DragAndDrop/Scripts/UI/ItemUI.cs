using UnityEngine;
using UnityEngine.EventSystems;

namespace DragAndDrop {

    public class ItemUI : MonoBehaviour, IDragable, IBeginDragHandler, IDragHandler, IEndDragHandler {

        public string DragableName => Type.ToString();
        public ItemType Type => itemType;
        public Sprite Sprite => itemSprite;

        [SerializeField] private ItemType itemType;
        [SerializeField] private Sprite itemSprite;

        private DragDropService dragDropService;

        private void Awake() {
            dragDropService = ServiceManager.GetService<DragDropService>();
        }

        public void OnBeginDrag(PointerEventData eventData) {
            dragDropService.StartDrag(this);
        }

        public void OnEndDrag(PointerEventData eventData) {
            dragDropService.ReleaseDrag();
        }

        public void OnDrag(PointerEventData eventData) {
        }

        public enum ItemType {
            Sword,
            Shield,
            Helmet
        }
    }
}