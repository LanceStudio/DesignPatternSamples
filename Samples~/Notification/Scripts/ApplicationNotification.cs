using UnityEngine;

namespace Notification {

    [DefaultExecutionOrder(-1)]
    public class ApplicationNotification : MonoBehaviour {

        [SerializeField] private NotificationService notificationService;

        private void Awake() {
            ServiceManager.ClearServices();
            ServiceManager.AddService(notificationService);
        }
    }
}