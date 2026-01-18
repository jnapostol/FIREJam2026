using UnityEngine;

public class GlassCabinet : MonoBehaviour
{
    [SerializeField] private GameObject _brokenObj;
    [SerializeField] private Animator _anim;
    [SerializeField] private Animator _swordAnim;
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ice(Clone)")
        {
            this.gameObject.GetComponent<Collider>().enabled = false;
            _brokenObj.SetActive(true);
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            _swordAnim.Play("Fall");
        }
    }
}
