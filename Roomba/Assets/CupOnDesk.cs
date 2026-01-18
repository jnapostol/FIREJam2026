using System.Collections;
using UnityEngine;

public class CupOnDesk : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] GameObject _turnOnGrabRuler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RacketInPlace")
        {
            AudioManager.Instance.PlayResource(8);
            _anim.Play("CupFallOffDesk");
            StartCoroutine(TurnOnGrabRuler());
        }
    }

    IEnumerator TurnOnGrabRuler()
    {
        yield return new WaitForSeconds(1.5f);
        _turnOnGrabRuler.SetActive(true);
    }
}
