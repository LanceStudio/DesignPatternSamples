using UnityEngine;

namespace DragAndDrop {

    [DefaultExecutionOrder(-1)]
    public class ApplicationDragAndDrop : MonoBehaviour {

        [SerializeField] private DragDropService dragAndDropService;

        private void Awake() {
            ServiceManager.ClearServices();
            ServiceManager.AddService(dragAndDropService);
        }
    }
}