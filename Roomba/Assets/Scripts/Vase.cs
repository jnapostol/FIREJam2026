using UnityEngine;

public class Vase : MonoBehaviour
{
    private Collider _col;
    private Animator _animator;
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
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
