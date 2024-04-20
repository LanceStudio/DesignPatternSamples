using System.Collections.Generic;
using UnityEngine;

namespace UndoRedo {

    /// <summary>
    /// The UndoRedo service manages two stacks, one for undo commands and one for redo commands.
    /// We had the ability to exectute multiple commands at once and group them into a single command,
    /// this allow the user to undo/redo all of them with a single Undo/Redo call.
    /// This functionality could have be done differently, for example it's possible to call a function like 
    /// "StartGroupCommand", then execute a bunch of commands (possibly on multiple frames), then call "StopGroupCommand" to group all this commands 
    /// into a single one and add it to the undo stack.
    /// The service manages inputs to trigger undo/redo, it could be done in another script as well.
    /// Note that commands are executed by the service, but it's totally fine to execute them outside and add it with a function like
    /// "PushCommand", you just have to be carefull to keep execution and Undo/Redo service in sync.
    /// </summary>
    public class UndoRedoService : MonoBehaviour {

        private readonly Stack<IUndoCommand> undoCommands = new();
        private readonly Stack<IUndoCommand> redoCommands = new();


        private void Update() {
            if(Input.GetKeyDown(KeyCode.U)) {
                PerformUndo();
            } else if(Input.GetKeyDown(KeyCode.R)) {
                PerformRedo();
            }
        }


        public void ExecuteCommand(IUndoCommand command) {
            if(command == null)
                return;

            command.Execute();
            undoCommands.Push(command);
            redoCommands.Clear();
        }

        public void ExecuteCommands(params IUndoCommand[] commands) {
            if(commands == null || commands.Length == 0)
                return;

            GroupUndoCommand groupCommand = new(commands);
            groupCommand.Execute();
            undoCommands.Push(groupCommand);
            redoCommands.Clear();
        }

        public void ExecuteCommands(List<IUndoCommand> commands) {
            ExecuteCommands(commands.ToArray());
        }


        public void PerformUndo() {
            if(undoCommands.TryPop(out IUndoCommand command)) {
                command.UnExecute();
                redoCommands.Push(command);
            }
        }

        public void PerformRedo() {
            if(redoCommands.TryPop(out IUndoCommand command)) {
                command.Execute();
                undoCommands.Push(command);
            }
        }
    }
}