using Unity.VisualScripting;
using UnityEngine;

namespace UndoRedoPlugin.Example
{
    /// <summary>
    /// Script for drag and drop any object at scene and add this actions into the storage
    /// </summary>
    public class ObjectsPositionController : MonoBehaviour
    {
        private const int RAY_DIRACTION_COEFFICIENT = 10;
        
        [SerializeField] private UndoRedoStorage _storage;
        
        private Transform _target;
        private bool _haveTarget;
        private Vector3 _targetStartPosition;

        private Vector3 _screenSpace;
        private Vector3 _offset;
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
            _storage ??= FindObjectOfType<UndoRedoStorage>();
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                RaycastHit hitInfo;
                _target = GetTargetObject (out hitInfo);

                if (_haveTarget)
                {
                    Vector3 targetPosition = _target.transform.position;
                    _screenSpace = _mainCamera.WorldToScreenPoint (targetPosition);
                    _offset = targetPosition - _mainCamera.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _screenSpace.z));
                }
            }

            if (Input.GetMouseButtonUp((int)MouseButton.Left) && _target)
            {
                _haveTarget = false;
                SaveAction(_targetStartPosition);
            }

            if (_haveTarget)
            {
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenSpace.z);
                Vector3 currentPosition = _mainCamera.ScreenToWorldPoint(currentScreenSpace) + _offset;
                _target.transform.position = currentPosition;
            }
        }

        private void SaveAction(Vector3 targetPosition)
        {
            IUndoable undoableAction = new ChangeTransformPositionAction(_target, targetPosition);
            _storage.AddAction(undoableAction);
        }

        private Transform GetTargetObject(out RaycastHit hit)
        {
            Transform target;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast (ray.origin, ray.direction * RAY_DIRACTION_COEFFICIENT, out hit)) {
                target = hit.collider.transform;
                _haveTarget = true;
                _targetStartPosition = target.position;
                return target;
            }
            return null;
        }
    }
}