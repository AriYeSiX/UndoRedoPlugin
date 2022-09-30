using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UndoRedoPlugin.Example
{
    /// <summary>
    /// Automatically subscribe on click button event and allow make your own realisation
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class AbstractButtonClickEvent : MonoBehaviour
    {
        protected Button _button;
        
        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClickAction);
        }

        protected virtual void OnDestroy() =>
            _button.onClick.RemoveListener(OnButtonClickAction);

        protected abstract void OnButtonClickAction();
    }
}