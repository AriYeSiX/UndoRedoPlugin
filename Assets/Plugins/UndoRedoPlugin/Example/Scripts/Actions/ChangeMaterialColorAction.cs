using UnityEngine;

namespace UndoRedoPlugin.Example
{
    /// <summary>
    /// realisation of undoable change material color action
    /// </summary>
    public class ChangeMaterialColorAction : IUndoable
    {
        private readonly Material _currentMaterial;
        private readonly Color _previousColor;
        private readonly Color _desiredColor;
        
        public void BaseAction() =>
            ChangeMaterialColor(_desiredColor);

        public void UndoAction() =>
            ChangeMaterialColor(_previousColor);

        private void ChangeMaterialColor(Color color) =>
            _currentMaterial.color = color;
        
        public ChangeMaterialColorAction(Material currentMaterial, Color desiredColor)
        {
            _currentMaterial = currentMaterial;
            _previousColor = currentMaterial.color;
            _desiredColor = desiredColor;
        }
    }
}