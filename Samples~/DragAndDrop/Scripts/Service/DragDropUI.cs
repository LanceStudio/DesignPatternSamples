using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DragAndDrop {

    public class DragDropUI : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI dragableNameText;
        [SerializeField] private TextMeshProUGUI dropableNameText;
        [SerializeField] private Image dropableBackground;
        [SerializeField] private Color normalColor;
        [SerializeField] private Color allowedColor;
        [SerializeField] private Color forbiddenColor;

        public void SetText(string itemText) {
            dragableNameText.text = itemText;
        }

        public void SetDropState(DropState dropState, string dropableName = "") {
            dropableBackground.gameObject.SetActive(dropState != DropState.None);
            dropableNameText.text = $"Move to {dropableName}";
            dropableBackground.color = dropState switch {
                DropState.None => normalColor,
                DropState.Forbidden => forbiddenColor,
                DropState.Allowed => allowedColor,
                _ => forbiddenColor,
            };
        }

        public enum DropState {
            None,
            Forbidden,
            Allowed
        }
    }
}