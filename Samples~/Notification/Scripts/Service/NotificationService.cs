using System;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Notification {

    public class NotificationService : MonoBehaviour {

        [Header("References")]
        [SerializeField] private Canvas notificationCanvas;
        [SerializeField] private GraphicRaycaster canvasGraphicRaycaster;
        [SerializeField] private LayoutElement contentLayoutElement;
        [SerializeField] private Image overlayImage;

        [Header("Settings")]
        [SerializeField] private Notification notificationPrefab = default;
        [SerializeField, Range(0, 1)] private float overlayOpacity = 0.5f;
        [SerializeField, Range(0, 1)] private float overlayTransitionDuration = 0.2f;

        public bool IsNotificationDisplayed => notifications.Count > 0;
        private readonly List<Notification> notifications = new();
        private CancellationTokenSource overlayCancellationSource;
        private bool overlayDisplayed = false;

        private void Awake() {
            overlayImage.gameObject.SetActive(false);
        }

        private void Update() {
            if(canvasGraphicRaycaster.enabled != IsNotificationDisplayed) {
                canvasGraphicRaycaster.enabled = IsNotificationDisplayed;
            }
            if(!Mathf.Approximately(contentLayoutElement.preferredHeight, notificationCanvas.pixelRect.height)) {
                contentLayoutElement.preferredHeight = notificationCanvas.pixelRect.height;
            }
            if(overlayDisplayed && !notifications.Exists(elem => elem.Type == NotificationType.BlockingError)) {
                overlayCancellationSource?.Cancel();
                overlayCancellationSource = new CancellationTokenSource();
                _ = HideOverlay(overlayCancellationSource.Token);
            }
        }

        private async Task DisplayOverlay(CancellationToken ct) {
            if(overlayDisplayed) {
                return;
            }

            overlayDisplayed = true;
            overlayImage.gameObject.SetActive(true);
            float startTime = Time.time;
            float progress = 0f;
            while(progress < 1f) {
                progress = Mathf.Clamp01((Time.time - startTime) / overlayTransitionDuration);
                Color color = overlayImage.color;
                color.a = Mathf.Lerp(0f, overlayOpacity, progress);
                overlayImage.color = color;
                await Task.Yield();
                if(ct.IsCancellationRequested) {
                    return;
                }
            }
        }

        private async Task HideOverlay(CancellationToken ct) {
            if(!overlayDisplayed) {
                return;
            }

            overlayDisplayed = false;
            float startTime = Time.time;
            float progress = 0f;
            while(progress < 1f) {
                progress = Mathf.Clamp01((Time.time - startTime) / overlayTransitionDuration);
                Color color = overlayImage.color;
                color.a = Mathf.Lerp(overlayOpacity, 0f, progress);
                overlayImage.color = color;
                await Task.Yield();
                if(ct.IsCancellationRequested) {
                    return;
                }
            }
            overlayImage.gameObject.SetActive(false);
        }

        public void Notify(string title, string message, NotificationType type = NotificationType.Neutral, float duration = 5) {
            if(type == NotificationType.BlockingError) {
                overlayCancellationSource?.Cancel();
                overlayCancellationSource = new CancellationTokenSource();
                _ = DisplayOverlay(overlayCancellationSource.Token);
            }

            Notification notification = Instantiate(notificationPrefab);
            notification.transform.SetParent(contentLayoutElement.transform);
            notification.Title = title;
            notification.Message = message;
            notification.Type = type;
            notification.Duration = type == NotificationType.BlockingError ? -1 : duration;
            notification.OnNotificationClosed += () => notifications.Remove(notification);
            notifications.Add(notification);
        }
    }

    public enum NotificationType {
        BlockingError,
        LowError,
        Neutral,
        Positive
    }
}
