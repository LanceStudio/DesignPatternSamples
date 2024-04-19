using UnityEngine;
using UnityEngine.UI;

namespace UndoRedo {

    public class ColorPreview : MonoBehaviour {

        [SerializeField] private Graphic previewGraphic;
        [SerializeField] private ColorPicker colorPicker;

        private void Start() {
            previewGraphic.color = colorPicker.Color;
            colorPicker.OnColorChanged += OnColorChanged;
        }

        public void OnColorChanged(Color c) {
            previewGraphic.color = c;
        }

        private void OnDestroy() {
            if(colorPicker != null)
                colorPicker.OnColorChanged -= OnColorChanged;
        }
    }
}