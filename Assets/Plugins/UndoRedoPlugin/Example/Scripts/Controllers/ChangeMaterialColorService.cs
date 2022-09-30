using UnityEngine;

namespace UndoRedoPlugin.Example
{
    /// <summary>
    /// Script for adding any material color change into the pull
    /// </summary>
    public class ChangeMaterialColorService : MonoBehaviour
    {
        [SerializeField] private UndoRedoStorage _storage;
        
        private void Start() => 
            _storage ??= FindObjectOfType<UndoRedoStorage>();

        /// <summary>
        /// Add change material action into storage
        /// </summary>
        public void ChangeColor(Material material, Color color)
        {
            IUndoable action = new ChangeMaterialColorAction(material, color);
            _storage.AddAction(action);
        }
    }
}