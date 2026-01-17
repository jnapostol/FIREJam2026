using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // repopulate selection manager array when they enter a trigger

    [SerializeField] private SmartObject _currentSmartObject;

    [SerializeField] private List<SmartObject> _selectableSmartObjects;

    public static SelectionManager Instance;
    [SerializeField] private int _index = 0;
    private void Start()
    {
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

    /// <summary>
    /// Called by InputAction Map
    /// </summary>
    void OnPrevious()
    {
        Debug.Log("Previous");

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
    void OnNext()
    {
        Debug.Log("Next");

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
