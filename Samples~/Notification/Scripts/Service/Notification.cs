using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Notification {

    public class Notification : MonoBehaviour {

        public event Action OnNotificationClosed;

        [SerializeField] private Animator notificationAnimator;
        [SerializeField] private RectTransform animatedContent;
        [SerializeField] private LayoutElement contentLayout;
        [SerializeField] private Button closeButton;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Image iconImage;
        [SerializeField] private List<Color> notificationTypesColors;

        public float Duration { get; set; } = 5f;

        private string title;
        public string Title {
            get => title;
            set {
                title = value;
                titleText.text = title;
            }
        }

        private string message;
        public string Message {
            get => message;
            set {
                message = value;
                messageText.text = message;
            }
        }

        private NotificationType type;
        public NotificationType Type {
            get => type;
            set {
                type = value;
                iconImage.color = notificationTypesColors[(int) type];
            }
        }


        private void Start() {
            closeButton.gameObject.SetActive(Type == NotificationType.BlockingError);
            if(Type != NotificationType.BlockingError) {
                _ = CloseAndDestroy(Duration);
            }
        }

        private void Update() {
            if(!Mathf.Approximately(contentLayout.preferredHeight, animatedContent.rect.height)) {
                contentLayout.preferredHeight = animatedContent.rect.height;
            }
        }

        private async Task CloseAndDestroy(float delay) {
            await Task.Delay((int) (delay * 1000));
            if(destroyCancellationToken.IsCancellationRequested) {
                return;
            }
            notificationAnimator.SetBool("hideNotification", true);
            while(!notificationAnimator.GetCurrentAnimatorStateInfo(0).IsName("EndState")) {
                await Task.Yield();
                if(destroyCancellationToken.IsCancellationRequested) {
                    return;
                }
            }
            OnNotificationClosed?.Invoke();
            Destroy(gameObject);
        }

        public void CloseNotification() {
            _ = CloseAndDestroy(0f);
        }
    }
}
