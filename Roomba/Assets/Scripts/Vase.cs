using UnityEngine;

public class Vase : MonoBehaviour
{
    private Collider _col;
    private Animator _animator;
    private bool _isBroken;
    private void Awake()
    {
        _col = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Baseball")
        {
            _animator.Play("Broken");
            Destroy(collision.gameObject);
            _isBroken = true;
        }
        if (collision.gameObject.CompareTag("Player") && _isBroken)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }


    }
}
