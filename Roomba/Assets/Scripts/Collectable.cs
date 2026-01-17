using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private const string PLAYER = "Player";
    
    [SerializeField] private bool _isCollectable;
    [SerializeField] private bool _isShootable;
    [SerializeField] private bool _isAttachable;
    [SerializeField] private GameObject _shootablePrefab;
    
    private bool _isAttached;
    private MeshRenderer _mesh;
    private Collider _col;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        _col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER))
        {
            if (_isShootable)
            {
                AddToInventory();
            }

            if (_isAttachable)
            {
                if (_isAttached)
                {
                    return;
                }
                else
                {
                    _isAttached = true;
                    SendAttachment();
                }
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
        if (_shootablePrefab != null)
        {
            // TODO: add shootable prefab to the inventory
            
        }
    }

    /// <summary>
    /// Sends attachable collectable to InventoryManager
    /// </summary>
    private void SendAttachment()
    {
        InventoryManager.Instance.SetCurrentAttachment(this.gameObject);
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
        _mesh.enabled = false;
        _col.enabled = false;
        StartCoroutine(WaitABit());
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Small delay, used in DestroySelf to destroy obj
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitABit()
    {
        yield return new WaitForSeconds(.5f);
    }
}
