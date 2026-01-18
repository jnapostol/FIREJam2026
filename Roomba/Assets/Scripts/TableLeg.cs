using UnityEngine;

public class TableLeg : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    bool _hasPlayed;
    public GameObject Sword;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Sword") && _hasPlayed == false)
        {
            _anim.Play("Fall");
            //InventoryManager.Instance.RemoveCurrentAttachment();
            if (Sword != null)
            {
                Sword.SetActive(false);
            }
            
            _hasPlayed = true;
        }
    }
}
