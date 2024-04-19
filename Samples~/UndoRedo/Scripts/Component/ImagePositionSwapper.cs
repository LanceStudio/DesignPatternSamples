using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UndoRedo {

    /// <summary>
    /// Changes images order randomly.
    /// We create commands dynamically, then execute them as a group.
    /// </summary>
    public class ImagePositionSwapper : MonoBehaviour {

        [SerializeField] private List<RectTransform> images;

        public void SwapImages() {
            List<int> availablePosition = Enumerable.Range(0, images.Count).ToList();
            List<IUndoCommand> commands = new();

            foreach (var image in images) {
                int randomPositionIndex = Random.Range(0, availablePosition.Count);
                int randomPosition = availablePosition[randomPositionIndex];
                int previousPosition = image.GetSiblingIndex();

                commands.Add(new DynamicCommand(
                    executeAction: () => image.SetSiblingIndex(randomPosition),
                    unExecuteAction: () => image.SetSiblingIndex(previousPosition)
                    ));
                availablePosition.RemoveAt(randomPositionIndex);
            }

            ServiceManager.GetService<UndoRedoService>().ExecuteCommands(commands);
        }
    }
}