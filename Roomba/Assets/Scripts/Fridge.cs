using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField] private GameObject _ice;
    [SerializeField] private Transform _spawn;

    public void CreateIce()
    {
        _ice.SetActive(true);
        GameObject ice = Instantiate(_ice);
        ice.SetActive(true);
    }
}
