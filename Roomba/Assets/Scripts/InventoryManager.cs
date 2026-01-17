using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Collectable _currentAttachment;
    [SerializeField] private Transform _attachmentPoint;
    [SerializeField] private Transform _spawnpoint;
    private Stack<Collectable> _throwables = new Stack<Collectable>();
    private List<GameObject> _throwableUI = new List<GameObject>();
    private LayerMask _playerMask, _throwableMask;
    public static InventoryManager Instance;

    private void Awake()
    {
        Instance = this;
        _playerMask = LayerMask.GetMask("Player");
        _throwableMask = LayerMask.GetMask("Throwable");

    }

    /// <summary>
    /// Called by Collectable upon player trigger enter
    /// </summary>
    /// <param name="throwable"></param>
    public void AddToThrowables(Collectable throwable)
    {
        throwable.gameObject.layer = 8;
        throwable.GetComponent<Rigidbody>().useGravity = false;
        _throwables.Push(throwable);
        Vector3 newPos = new Vector3 (_spawnpoint.transform.position.x, _spawnpoint.transform.position.y - 1.5f * _throwables.Count, _spawnpoint.transform.position.z);
        throwable.GetComponent<Rigidbody>().excludeLayers = _throwableMask;
        throwable.transform.position = newPos;
        throwable.transform.rotation = Quaternion.identity;
        throwable.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        throwable.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    /// <summary>
    /// Called by Player Controller to get the last throwable from stack
    /// </summary>
    /// <returns></returns>
   
    public Collectable PopThrowable()
    {
        return _throwables.Pop();
        //return _throwables.Peek();
    }

    public bool HasThrowable()
    {
        if (_throwables.Count > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Called by the Player Controller to update the stack's visuals
    /// </summary>
    public void UpdateVisuals()
    {
        if (HasThrowable() == false)
        {
            return;
        }

        Collectable[] throwArr = _throwables.ToArray();

        if (_throwables.Count == 1)
        {
            return;
        }

        for (int i = 2; i < _throwables.Count; i++)
        {
            Debug.Log("updating visual after pop");
            throwArr[i].gameObject.transform.position = new Vector3(throwArr[i].gameObject.transform.position.x, _spawnpoint.transform.position.y + 1.5f * throwArr.Length, throwArr[i].gameObject.transform.position.z);
        }
    }

    public Collectable GetCurrentAttachment()
    {
        return _currentAttachment;
    }

    /// <summary>
    /// Called by Collectable to update inventory manager's current attachment
    /// </summary>
    /// <param name="attachment"></param>
    public void SetCurrentAttachment(Collectable attachment)
    {
        _currentAttachment = attachment;
        _currentAttachment.gameObject.transform.parent = _attachmentPoint;
        _currentAttachment.gameObject.transform.localPosition = Vector3.zero;
    }
}
