using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField] private GameObject _ice;
    [SerializeField] private Transform _spawn;

    public void CreateIce()
    {
        GameObject ice = Instantiate(_ice, _spawn);
        ice.SetActive(true);
    }
}
