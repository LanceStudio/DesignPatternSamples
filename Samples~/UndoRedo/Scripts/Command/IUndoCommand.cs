namespace UndoRedo {

    /// <summary>
    /// Classic interface for command pattern.
    /// </summary>
    public interface IUndoCommand {

        void Execute();

        void UnExecute();
    }
}