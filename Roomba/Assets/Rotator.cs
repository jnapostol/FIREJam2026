using UnityEngine;

public class Rotator : MonoBehaviour
{
    bool _isRotating = false;

    private void Start()
    {
        
    }
    public void ToggleRotate()
    {
        Debug.Log("ToggleRotate");
        if (_isRotating)
        {
            _isRotating = false;
            transform.GetChild(0).gameObject.SetActive(false);
        } else
        {
            _isRotating = true;
            transform.GetChild(0).gameObject.SetActive(true);
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
