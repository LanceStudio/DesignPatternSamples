using System.Linq;

namespace UndoRedo {

    /// <summary>
    /// This command allow us to group a bunch of commands into a single one.
    /// Note that the UnExecute function should iterate on reversed list, to undo the commands correctly (In case order matters)
    /// </summary>
    public class GroupUndoCommand : IUndoCommand {

        private readonly IUndoCommand[] m_commands;

        public GroupUndoCommand(IUndoCommand[] commands) {
            m_commands = commands;
        }

        public void Execute() {
            foreach(IUndoCommand command in m_commands) {
                command.Execute();
            }
        }

        public void UnExecute() {
            foreach(IUndoCommand command in m_commands.Reverse()) {
                command.UnExecute();
            }
        }
    }
}