using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    // TODO: NEED TO CREATE PLAYER ACTION TO NAVIGATE UI INTERFACE

    [SerializeField] private SmartObject _currentSmartObject;
    [SerializeField] private List<SmartObject> _selectableSmartObjects;
    [SerializeField] private int _index = 0;
    private PlayerInput _input;
    public static SelectionManager Instance;
    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        // Select the first obj in the smart obj list
        _currentSmartObject = _selectableSmartObjects[_index];
        _currentSmartObject.SetSelected(true);
        _currentSmartObject.OnSelected();
    }

    /// <summary>
    /// Called by trigger volume when entering a new room
    /// </summary>
    /// <param name="newSmartObjs"></param>
    /// <param name="index"></param>
    public void RepopulateSmartObjects(List<SmartObject> newSmartObjs, int index)
    {
        _selectableSmartObjects.Clear();
        _selectableSmartObjects = newSmartObjs;
        _index = index;
    }

    public bool HasUISelected()
    {
        if (_currentSmartObject != null)
        {
            return _currentSmartObject.HasUI();
        }
        return false;
    }

    /// <summary>
    /// Called by InputAction Map
    /// </summary>
    private void OnPrevious()
    {
        // Deselect current smart obj
        _currentSmartObject.SetSelected(false);
        _currentSmartObject.OnDeselected();
        --_index;

        if (_index < 0)
        {
            _index = _selectableSmartObjects.Count-1;
        }

        // Select next smart obj in list
        _currentSmartObject = _selectableSmartObjects[_index];
        _currentSmartObject.SetSelected(true);
        _currentSmartObject.OnSelected();
    }

    /// <summary>
    /// Called by InputAction Map
    /// </summary>
    private void OnNext()
    {
        // Deselect current smart obj
        _currentSmartObject.SetSelected(false);
        _currentSmartObject.OnDeselected();
        ++_index;

        if(_index > _selectableSmartObjects.Count - 1)
        {
            _index = 0;
        }

        // Select next smart obj in list
        _currentSmartObject = _selectableSmartObjects[_index];
        _currentSmartObject.SetSelected(true);
        _currentSmartObject.OnSelected();
    }
}
