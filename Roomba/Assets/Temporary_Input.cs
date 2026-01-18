using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Temporary_Input : MonoBehaviour
{
    [SerializeField] Light _light; //light
    [SerializeField] Animator _anim;
    bool _animToggled = false;
    public UnityEvent OnPress;
    bool _rotating = false; //recordplayer
    [SerializeField] Transform _transformToSpin;
    
    private void FixedUpdate()
    {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            OnPress.Invoke();
        }
        if (_rotating)
        {
            _transformToSpin.Rotate(0f, 0.5f, 0f);
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

    public void ToggleRotation()
    {
        if (_rotating)
        {
            _rotating = false;
        } else
        {
            _rotating = true;
        }
    }
}
