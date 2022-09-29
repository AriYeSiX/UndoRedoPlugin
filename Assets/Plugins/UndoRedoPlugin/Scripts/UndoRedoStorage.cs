using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace UndoRedoPlugin
{
    /// <summary>
    /// Storage for collect any actions and transfer them between undo and redo stacks
    /// </summary>
    public class UndoRedoStorage : MonoBehaviour
    {
        private readonly Stack<IUndoable> _undoActionsStack = new Stack<IUndoable>();
        /// <summary>
        /// Stack for storage all undo actions
        /// </summary>
        public IReadOnlyCollection<IUndoable> UndoActionsStack => _undoActionsStack;
        
        private readonly Stack<IUndoable> _redoActionsStack = new Stack<IUndoable>();
        /// <summary>
        /// Stack for storage all redo actions
        /// </summary>
        public IReadOnlyCollection<IUndoable> RedoActionsStack => _redoActionsStack;
        
        /// <summary>
        /// Action while Undo() was called
        /// </summary>
        public event Action UndoAction;
        
        /// <summary>
        /// Action while Redo() was called
        /// </summary>
        public event Action RedoAction;

        /// <summary>
        /// Action while AddAction was called
        /// </summary>
        public event Action StackUpdate;
        
        /// <summary>
        /// Add new action to undo stack and run this action
        /// </summary>
        /// <param name="undoableAction">Any interface realisation</param>
        public void AddAction(IUndoable undoableAction)
        {
            _undoActionsStack.Push(undoableAction);
            undoableAction.BaseAction();
            //Clear redo stack if Stack.count > 0
            ClearAnyStack(_redoActionsStack);
            StackUpdate?.Invoke();
        }

        /// <summary>
        /// Make undo action and move it to redo stack
        /// </summary>
        public void Undo()
        {
            ChangeActionStack(_undoActionsStack, _redoActionsStack).UndoAction();
            UndoAction?.Invoke();
        }
        
        /// <summary>
        /// Make redo action and move it to undo stack
        /// </summary>
        public void Redo()
        {
            ChangeActionStack(_redoActionsStack, _undoActionsStack).BaseAction();
            RedoAction?.Invoke();
        }
        
        /// <summary>
        /// Clear undo and redo stacks
        /// </summary>
        public void ClearStacks()
        {
            ClearAnyStack(_undoActionsStack);
            ClearAnyStack(_redoActionsStack);
        }

        private void ClearAnyStack(Stack<IUndoable> stack)
        {
            if (stack.Count > 0)
                stack.Clear();
        }
        
        private IUndoable ChangeActionStack(Stack<IUndoable> popStack, Stack<IUndoable> pushStack)
        {
            IUndoable action = popStack.Pop();
            pushStack.Push(action);
            return action;
        }
        
    }
}

