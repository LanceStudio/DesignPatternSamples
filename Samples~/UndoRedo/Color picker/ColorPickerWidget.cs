using UnityEngine;
using UnityEngine.UI;

namespace UndoRedo {

    public class ColorPickerWidget : MonoBehaviour {

        public delegate void ColorChangedCallback(Color color);

        [SerializeField] private ColorPicker colorPicker;
        [SerializeField] private Button applyButton;
        [SerializeField] private Button cancelButton;

        private ColorChangedCallback m_ApplyAction;
        private ColorChangedCallback m_CancelAction;
        private ColorChangedCallback m_ColorChangedAction;


        private void Awake() {
            applyButton.onClick.AddListener(ApplyColor);
            cancelButton.onClick.AddListener(CancelColor);
            colorPicker.OnColorChanged += ColorChange;
            Close();
        }

        private void OnDestroy() {
            if(applyButton != null)
                applyButton.onClick.RemoveListener(ApplyColor);
            if(cancelButton != null)
                cancelButton.onClick.RemoveListener(CancelColor);
            if(colorPicker != null)
                colorPicker.OnColorChanged -= ColorChange;
        }

        private void ApplyColor() {
            m_ApplyAction?.Invoke(colorPicker.Color);
            Close();
        }

        private void CancelColor() {
            m_CancelAction?.Invoke(colorPicker.Color);
            Close();
        }

        private void ColorChange(Color color) {
            m_ColorChangedAction?.Invoke(color);
        }


        public void Open(Color defaultColor, ColorChangedCallback applyAction, ColorChangedCallback cancelAction, ColorChangedCallback colorChangedAction) {
            m_ApplyAction = applyAction;
            m_CancelAction = cancelAction;
            m_ColorChangedAction = colorChangedAction;
            colorPicker.Color = defaultColor;
            gameObject.SetActive(true);
        }

        public void Close() {
            m_ApplyAction = null;
            m_CancelAction = null;
            m_ColorChangedAction = null;
            gameObject.SetActive(false);
        }
    }
}