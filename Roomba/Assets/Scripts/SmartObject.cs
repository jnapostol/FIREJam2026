using MoreMountains.Tools;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SmartObject : MonoBehaviour
{
    [SerializeField] private UnityEvent _onSelected;
    [SerializeField] private UnityEvent _onDeselect;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private bool _hasUI;
    [SerializeField] private Renderer _renderer;
    private bool _selected;
    private Color _colorStart;
    private Coroutine _lerpColor;

    private void Awake()
    {
        if (this.gameObject.CompareTag("Player")) return;
        
        if (_renderer == null)
        {
            _renderer = GetComponent<MeshRenderer>();
        }
        if (_arrow!= null)
        {
            _colorStart = _arrow.GetComponent<MeshRenderer>().material.color;
        }
        
    }
 
    public bool HasUI() {  return _hasUI; }

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
            _lerpColor = StartCoroutine(LerpArrowOpacity());
            if (_renderer.materials.Length == 1)
            {
                _renderer.materials[0].SetFloat("_Scale", 1.1f);
            }
            else
            {
                _renderer.materials[1].SetFloat("_Scale", 1.1f);
            }
        }
        else
        {
            // Turn off outline
            _arrow.SetActive(false);
            StopCoroutine(_lerpColor);
            if (_renderer.materials.Length == 1)
            {
                _renderer.materials[0].SetFloat("_Scale", 0f);
            }
            else
            {
                _renderer.materials[1].SetFloat("_Scale", 0f);
            }
        }
    }

    IEnumerator LerpArrowOpacity()
    {
        if (_arrow == null)
        {
            yield break;
        }
        Debug.Log("LerpArrowOpacity reached!");
        MeshRenderer rend = _arrow.gameObject.GetComponent<MeshRenderer>();
        float timePassed = 0;
        //rend.material.color.a = 1f;
        //Color startingColor = rend.material.color;
        while (timePassed < 4f)
        {
            Color c = Color.Lerp(_colorStart, Color.clear, (timePassed / 4f));
            rend.material.color = c;
            yield return null;
            timePassed += Time.deltaTime;
        }
        _arrow.SetActive(false);
    }
}
