namespace UndoRedoPlugin
{
    /// <summary>
    /// interface for adding undo action to any realisation
    /// </summary>
    public interface IUndoable
    {
        public void BaseAction();
        public void UndoAction();
    }
}