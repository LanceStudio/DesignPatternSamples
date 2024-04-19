using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UndoRedo {

    /// <summary>
    /// Allow the user to change the color of the image with a ColorPicker.
    /// I choose to put all commands related to a class in it. So it's available only through MyClass.MyCommand.
    /// If can change the command class access modifier to suit your needs, for example :
    ///  - If you want commands accessible from the outside like this one, set it to public.
    ///  - If you want commands to be accessible only from the Parent class, set it to private.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class ImageColorModifier : MonoBehaviour, IPointerClickHandler {

        [SerializeField] private ColorPickerWidget colorPicker;

        private Image image;
        private Color currentColor;

        void Awake() {
            image = GetComponent<Image>();
            currentColor = image.color;
        }

        private void ApplyColorUndoable(Color color) {
            ApplyColorCommand command = new(this, color);
            ServiceManager.GetService<UndoRedoService>().ExecuteCommand(command);
        }

        private void ApplyColor(Color color) {
            currentColor = color;
            image.color = currentColor;
        }

        private void CancelColor(Color color) {
            image.color = currentColor;
        }

        private void PreviewColor(Color color) {
            image.color = color;
        }

        public void OnPointerClick(PointerEventData eventData) {
            colorPicker.Open(currentColor, ApplyColorUndoable, CancelColor, PreviewColor);
        }


        /// <summary>
        /// A command is a simple class.
        /// All it needs should be passed through the constructor.
        /// </summary>
        public class ApplyColorCommand : IUndoCommand {

            private ImageColorModifier m_imageColorModifier;
            private Color m_color;
            private Color m_undoColor;

            public ApplyColorCommand(ImageColorModifier imageColorModifier, Color color) {
                m_imageColorModifier = imageColorModifier;
                m_color = color;
                m_undoColor = imageColorModifier.currentColor;
            }

            public void Execute() {
                m_imageColorModifier.ApplyColor(m_color);
            }

            public void UnExecute() {
                m_imageColorModifier.ApplyColor(m_undoColor);
            }
        }
    }
}