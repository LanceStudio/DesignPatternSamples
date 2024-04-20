using System;

namespace UndoRedo {

    /// <summary>
    /// Dynamic command allows us to create command whose the content is not known in advance.
    /// So it may depends on the context execution (User inputs, external data etc...).
    /// </summary>
    public class DynamicCommand : IUndoCommand {

        private readonly Action m_executeAction;
        private readonly Action m_unExecuteAction;

        public DynamicCommand(Action executeAction, Action unExecuteAction) {
            m_executeAction = executeAction;
            m_unExecuteAction = unExecuteAction;
        }

        public void Execute() {
            m_executeAction.Invoke();
        }

        public void UnExecute() {
            m_unExecuteAction.Invoke();
        }
    }
}