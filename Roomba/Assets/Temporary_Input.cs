using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Temporary_Input : MonoBehaviour
{
    [SerializeField] Light _light; //light
    [SerializeField] Animator _anim;
    bool _animToggled = false;
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

    public void ToggleAnimation(string animName)
    {
        if (!_animToggled)
        {
            //play animation
            _anim.Play(animName);
            _animToggled = true;
        }
    }
}
