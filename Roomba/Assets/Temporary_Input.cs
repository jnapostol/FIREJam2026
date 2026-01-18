using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Temporary_Input : MonoBehaviour
{
    [SerializeField] Light _light;
    public UnityEvent OnPress;
    private void Update()
    {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            OnPress.Invoke();
        }
    }
    public void ToggleLight()
    {
        if (_light.enabled)
        {
            _light.enabled = false;
        } else
        {
            _light.enabled = true;
        }
    }
}
