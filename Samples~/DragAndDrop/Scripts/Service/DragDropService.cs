using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragAndDrop {

    public class DragDropService : MonoBehaviour {

        [SerializeField] private DragDropUI dragDropUI;

        public bool IsDragging { get; private set; }
        public IDragable DraggedObject { get; private set; }
        public IDropable DropableZone { get; private set; }

        private readonly List<RaycastResult> hoveredUIs = new();

        private void Awake() {
            ReleaseDrag();
        }

        private void Update() {
            if(IsDragging) {
                UpdateMouseCursor();
                if(Input.GetKeyDown(KeyCode.Escape)) {
                    CancelDrag();
                }
            }
        }

        private void UpdateMouseCursor() {
            dragDropUI.transform.position = Input.mousePosition;
            RefreshHoveredUIElements();
            IDropable dropable = null;
            hoveredUIs.Exists((elem) => elem.gameObject.TryGetComponent(out dropable));
            DropableZone = dropable;
            if(DropableZone != null) {
                dragDropUI.SetDropState(DropableZone.CanDrop(DraggedObject) ? DragDropUI.DropState.Allowed : DragDropUI.DropState.Forbidden, dropable.DropableName);
            } else {
                dragDropUI.SetDropState(DragDropUI.DropState.None);
            }
        }

        private void RefreshHoveredUIElements() {
            hoveredUIs.Clear();

            if(EventSystem.current != null) {
                PointerEventData pointer = new(EventSystem.current) {
                    position = Input.mousePosition
                };
                EventSystem.current.RaycastAll(pointer, hoveredUIs);
            }
        }

        public void StartDrag(IDragable dragable) {
            if(dragable == null) {
                return;
            }
            IsDragging = true;
            dragDropUI.SetText(dragable.DragableName);
            dragDropUI.gameObject.SetActive(true);
            DraggedObject = dragable;
        }

        public void CancelDrag() {
            IsDragging = false;
            dragDropUI.gameObject.SetActive(false);
            DraggedObject = null;
            DropableZone = null;
        }

        public void ReleaseDrag() {
            if(DropableZone != null && DropableZone.CanDrop(DraggedObject)) {
                DropableZone.Drop(DraggedObject);
            }
            CancelDrag();
        }
    }
}
