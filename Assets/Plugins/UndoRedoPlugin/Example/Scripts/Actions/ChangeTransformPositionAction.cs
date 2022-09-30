using System;
using UnityEngine;

namespace UndoRedoPlugin.Example
{
    /// <summary>
    /// realisation of undoable change transform position action
    /// </summary>
    public class ChangeTransformPositionAction : IUndoable
    {
        private readonly Transform _changePositionTarget;
        private readonly Vector3 _desiredPosition;
        private readonly Vector3 _startedPosition;
        
        public void BaseAction() =>
            _changePositionTarget.position = _desiredPosition;

        public void UndoAction() =>
            _changePositionTarget.position = _startedPosition;
        
        public ChangeTransformPositionAction(Transform changePositionTarget, Vector3 startedPosition)
        {
            _changePositionTarget = changePositionTarget;
            _desiredPosition =  changePositionTarget.position;
            _startedPosition = startedPosition;
        }
    }
}