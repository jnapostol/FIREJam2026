using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;

public class SmartObject : MonoBehaviour
{
    [SerializeField] private UnityEvent _onSelected;
    [SerializeField] private UnityEvent _onDeselect;
    [SerializeField] private GameObject _arrow;
    private bool _selected;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
 
    /// <summary>
    /// Called by the SelectionManager upon pressing LB RB
    /// </summary>
    /// <param name="value"></param>
    public void SetSelected(bool value)
    {
        _selected = value;
    }

    /// <summary>
    /// Called to invoke the Public OnSelected Event on obj. Anything attached to the event gets called
    /// </summary>
    public void OnSelected()
    {
        _onSelected.Invoke();
    }

    /// <summary>
    /// Called to invoke the Public OnDeselected Event on obj. Anything attached to the event gets called
    /// </summary>
    public void OnDeselected()
    {
        _onDeselect.Invoke();
    }

    /// <summary>
    /// Called on Public OnSelected & OnDeselected Event on obj
    /// </summary>
    public void ToggleOutline()
    {
        if (_selected)
        {
            // Turn on outline
            _arrow.SetActive(true);
            _renderer.materials[1].SetFloat("_Scale", 1.1f);
        }
        else
        {
            // Turn off outline
            _arrow.SetActive(false);
            _renderer.materials[1].SetFloat("_Scale", 0f);
        }
    }
}
