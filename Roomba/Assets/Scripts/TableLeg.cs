using UnityEngine;

public class TableLeg : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Sword")
        {
            _anim.Play("Fall");
            InventoryManager.Instance.RemoveCurrentAttachment();
        }
    }
}
