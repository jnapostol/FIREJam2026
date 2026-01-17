using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private const string PLAYER = "Player";
    
    [SerializeField] private bool _isCollectable;
    [SerializeField] private bool _isShootable;
    [SerializeField] private bool _isAttachable;
    
    private bool _isAttached;
    private MeshRenderer _mesh;
    private Collider _col;
    private Rigidbody _rb;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        _col = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER))
        {
            if (_isAttachable)
            {
                if (_isAttached)
                {
                    return;
                }
                else
                {
                    _isAttached = true;
                    _col.isTrigger = false;
                    SendAttachment();
                }
            }

            if (_isCollectable)
            {
                DestroySelf();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(PLAYER))
        {
            if (_isShootable)
            {
                AddToInventory();
            }

            if (_isCollectable)
            {
                DestroySelf();
            }
        }
    }

    /// <summary>
    /// Adds shootable collectable to InventoryManager stack
    /// </summary>
    private void AddToInventory()
    {
        InventoryManager.Instance.AddToThrowables(this);
    }

    /// <summary>
    /// Sends attachable collectable to InventoryManager
    /// </summary>
    private void SendAttachment()
    {
        InventoryManager.Instance.SetCurrentAttachment(this);
    }

    /// <summary>
    /// Destroys mesh, unless collectable is attached
    /// </summary>
    private void DestroySelf()
    {
        if (_isAttached)
        {
            _isCollectable = false;
            return;
        }

        if(_isShootable)
        {
            return;
        }
         _mesh.enabled = false;
        _col.enabled = false;
        StartCoroutine(WaitABit());
        
    }

    /// <summary>
    /// Small delay, used in DestroySelf to destroy obj
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitABit()
    {
        yield return new WaitForSeconds(.8f);
        Destroy(this.gameObject);
    }
}
