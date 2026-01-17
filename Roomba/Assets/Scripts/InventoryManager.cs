using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // TODO: THROWABLE OBJECTS!

    [SerializeField] private GameObject _currentAttachment;
    [SerializeField] private Transform _attachmentPoint;
    private Stack<Collectable> _throwables;

    public static InventoryManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Called by Collectable upon player trigger enter
    /// </summary>
    /// <param name="throwable"></param>
    public void AddToThrowables(Collectable throwable)
    {
        _throwables.Push(throwable);
    }

    /// <summary>
    /// Called by Player Controller to use throwable from stack
    /// </summary>
    /// <returns></returns>
    public Collectable UseThrowable()
    {
        return _throwables.Pop();
    }

    public GameObject GetCurrentAttachment()
    {
        return _currentAttachment;
    }

    /// <summary>
    /// Called by Collectable to update inventory manager's current attachment
    /// </summary>
    /// <param name="attachment"></param>
    public void SetCurrentAttachment(GameObject attachment)
    {
        _currentAttachment = attachment;
        _currentAttachment.transform.parent = _attachmentPoint;
        _currentAttachment.transform.localPosition = Vector3.zero;
    }
}
