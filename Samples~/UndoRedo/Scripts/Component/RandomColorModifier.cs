using System.Collections.Generic;
using UnityEngine;

namespace UndoRedo {

    /// <summary>
    /// Update all images with random colors and group all this changes in a single command.
    /// </summary>
    public class RandomColorModifier : MonoBehaviour {

        [SerializeField] private List<ImageColorModifier> imageColorModifiers;

        public void ChangeColor() {
            List<IUndoCommand> commands = new();
            foreach(var colorModifier in imageColorModifiers) {
                commands.Add(new ImageColorModifier.ApplyColorCommand(colorModifier, Random.ColorHSV()));
            }
            ServiceManager.GetService<UndoRedoService>().ExecuteCommands(commands);
        }
    }
}