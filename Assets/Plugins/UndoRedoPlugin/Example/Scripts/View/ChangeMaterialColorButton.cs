using System;
using UnityEngine;
using UnityEngine.UI;

namespace UndoRedoPlugin.Example
{
    /// <summary>
    /// Send change material color command into the service and show active graphic if material color == buttonImage.color
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class ChangeMaterialColorButton : AbstractButtonClickEvent
    {
        [SerializeField] private Material _material;
        [SerializeField] private Graphic _targetGraphic;
        
        private Image _buttonImage;
        private Color _currentColor;
        private ChangeMaterialColorService _changeMaterialColorService;

        /// <summary>
        /// Action while change color button was clicked
        /// </summary>
        public event Action ChangeColorAction;
        
        protected override void Awake()
        {
            base.Awake();
            _buttonImage = GetComponent<Image>();
            _currentColor = _buttonImage.color;
            UpdateGraphicState();
        }

        private void Start() => 
            _changeMaterialColorService ??= FindObjectOfType<ChangeMaterialColorService>();

        /// <summary>
        /// Update active and inactive button graphic state
        /// </summary>
        public void UpdateGraphicState() =>
            SetGraphicAlpha(_material.color == _currentColor);
        
        protected override void OnButtonClickAction()
        {
            _changeMaterialColorService.ChangeColor(_material, _currentColor);
            UpdateGraphicState();
            ChangeColorAction?.Invoke();
        }

        private void SetGraphicAlpha(bool state) => 
            _targetGraphic.CrossFadeAlpha(state ? 1f : 0f, 0f, true);
    }
}