using UnityEngine;

namespace Notification {

    public class TestNotification : MonoBehaviour {

        private NotificationService notificationService;

        private void Awake() {
            notificationService = ServiceManager.GetService<NotificationService>();
        }

        public void NotifyBlockingError() {
            notificationService.Notify("Blocking Error", "An error occurred that requires immediate attention.", NotificationType.BlockingError);
        }

        public void NotifyLowError() {
            notificationService.Notify("Low Error", "An error occurred that requires attention.", NotificationType.LowError);
        }

        public void NotifyNeutral() {
            notificationService.Notify("Neutral", "This is a neutral notification.", NotificationType.Neutral);
        }

        public void NotifySuccess() {
            notificationService.Notify("Success", "The operation was completed successfully.", NotificationType.Positive);
        }
    }
}