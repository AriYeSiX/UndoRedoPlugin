using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndoRedoPlugin.Example
{
    /// <summary>
    /// Implementing an undo or redo action call through the UI
    /// </summary>
    public class UndoRedoButton : AbstractButtonClickEvent
    {
        [SerializeField] private UndoRedoStorage _undoRedoStorage;
        [SerializeField] private ActionButtonType _actionButtonType;

        protected override void Awake()
        {
            base.Awake();
            _undoRedoStorage ??= FindObjectOfType<UndoRedoStorage>();
            Subscribes();
            SetButtonInteractabilityStateByType();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Unsubsribes();
        }

        private void Subscribes()
        {
            _undoRedoStorage.StackUpdate += SetButtonInteractabilityStateByType;
            _undoRedoStorage.UndoAction += SetButtonInteractabilityStateByType;
            _undoRedoStorage.RedoAction += SetButtonInteractabilityStateByType;
        }

        private void Unsubsribes()
        {
            _undoRedoStorage.StackUpdate -= SetButtonInteractabilityStateByType;
            _undoRedoStorage.UndoAction -= SetButtonInteractabilityStateByType;
            _undoRedoStorage.RedoAction -= SetButtonInteractabilityStateByType;
        }
        
        protected override void OnButtonClickAction()
        {
            switch (_actionButtonType)
            {
                case ActionButtonType.UndoActionButton:
                    _undoRedoStorage.Undo();
                    break;
                case ActionButtonType.RedoActionButton:
                    _undoRedoStorage.Redo();
                    break;
            }
        }

        private void SetButtonInteractabilityStateByType()
        {
            switch (_actionButtonType)
            {
                case ActionButtonType.UndoActionButton:
                    SetButtonInteractableState(CheckStackNotEmpty(_undoRedoStorage.UndoActionsStack));
                    break;
                case ActionButtonType.RedoActionButton:
                    SetButtonInteractableState(_button.interactable = CheckStackNotEmpty(_undoRedoStorage.RedoActionsStack));
                    break;
            }
        }

        private void SetButtonInteractableState(bool state) =>
            _button.interactable = state;
        
        private bool CheckStackNotEmpty(IReadOnlyCollection<IUndoable> collection) => 
            collection.Count > 0;
    }
}