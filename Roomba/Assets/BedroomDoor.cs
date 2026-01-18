using UnityEngine;

public class BedroomDoor : MonoBehaviour
{
    bool _isPlayed;

    [SerializeField] Animator _anim;
    private void Start()
    {
       
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ruler" && !_isPlayed)
        {
            _anim.Play("BedroomDoorSwingOpen");
            _isPlayed = true;
        }
    }
}
