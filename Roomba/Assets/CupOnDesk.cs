using UnityEngine;

public class CupOnDesk : MonoBehaviour
{
    Animator _anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RacketInPlace")
        {
            Debug.Log("Hit Cupd wonn beasttttt");
        }
    }
}
