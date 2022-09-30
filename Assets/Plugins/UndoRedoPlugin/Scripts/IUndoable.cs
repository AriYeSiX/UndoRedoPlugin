namespace UndoRedoPlugin
{
    /// <summary>
    /// interface for adding undo action to any realisation
    /// </summary>
    public interface IUndoable
    {
        /// <summary>
        /// Base script action
        /// </summary>
        public void BaseAction();
        /// <summary>
        /// Revert at previous state action
        /// </summary>
        public void UndoAction();
    }
}