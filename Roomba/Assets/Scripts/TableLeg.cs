using UnityEngine;

public class TableLeg : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    bool _hasPlayed;
    public GameObject Sword;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Table oncollisionenter called");
        Debug.Log($"{collision.gameObject.name}, _hasPlayed = {_hasPlayed}");
        if (collision.gameObject.name.Contains("Player") && _hasPlayed == false && Sword.activeInHierarchy)
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
