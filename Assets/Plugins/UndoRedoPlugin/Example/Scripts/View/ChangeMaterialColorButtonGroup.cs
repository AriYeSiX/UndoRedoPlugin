using System.Collections.Generic;
using UnityEngine;

namespace UndoRedoPlugin.Example
{
    /// <summary>
    /// Group storage all change material color buttons and subscribe on change material color events for update active and inactive buttons
    /// </summary>
    public class ChangeMaterialColorButtonGroup : MonoBehaviour
    {
        [SerializeField] private List<ChangeMaterialColorButton> _changeMaterialColorButtons;
        [SerializeField] private UndoRedoStorage _storage;

        private void Start()
        {
            _storage ??= FindObjectOfType<UndoRedoStorage>();
            Subscribe();
        }

        private void OnDestroy() =>
            Unsubscribe();

        private void Subscribe()
        {
            _storage.UndoAction += OnChangeColorClick;
            _storage.RedoAction += OnChangeColorClick;
            
            foreach (ChangeMaterialColorButton button in _changeMaterialColorButtons)
                button.ChangeColorAction += OnChangeColorClick;
        }

        private void Unsubscribe()
        {
            _storage.UndoAction -= OnChangeColorClick;
            _storage.RedoAction -= OnChangeColorClick;
            
            foreach (ChangeMaterialColorButton button in _changeMaterialColorButtons)
                button.ChangeColorAction -= OnChangeColorClick;
        }

        private void OnChangeColorClick()
        {
            foreach (ChangeMaterialColorButton button in _changeMaterialColorButtons)
                button.UpdateGraphicState();
        }
    }
}