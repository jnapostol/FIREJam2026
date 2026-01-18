using UnityEngine;

public class Rotator : MonoBehaviour
{
    bool _isRotating = false;

    private void Start()
    {
        
    }
    public void ToggleRotate()
    {
        if (_isRotating)
        {
            _isRotating = false;
            transform.GetChild(0).gameObject.SetActive(true);
        } else
        {
            _isRotating = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRotating)
        {
            transform.Rotate(0, 0.2f, 0);
        }

    }
}
