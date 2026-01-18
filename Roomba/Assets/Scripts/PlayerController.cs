using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using NUnit.Framework.Internal.Commands;
using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 _movementVector;
    [SerializeField] Animator _animator;
    [SerializeField] float _rotateSpeed;
    [SerializeField] float _movementSpeed;
    [SerializeField] float _northYRotation;
    [SerializeField] float _shootForce;
    [SerializeField] Transform _launchPoint;
    [SerializeField] ParticleSystem _smokeBurstFX;
    [SerializeField] GameObject _smokeMovementFX;
    [SerializeField] GameObject _smokeCollideFX;

    float minImpactSpeed = 0.5f;
    bool _recentHurt = false;
    Rigidbody _rb;
    string requiredTag = "collectable";


    bool _canMove = false;

    [Header("Broken State Stuff")]
    bool _isBroken = true;
    [SerializeField] GameObject _screen;
    [SerializeField] bool _hasBattery;
    [SerializeField] bool _hasBandAid;
    [SerializeField] GameObject _brokenModel;
    [SerializeField] GameObject _fixedModel;
    [SerializeField] GameObject _electricityFX;
    Coroutine _flickerCoroutine;
    private LayerMask _playerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _canMove = false;

        //broken stuff
        _isBroken = true;
        if (_screen != null)
        {
            _flickerCoroutine = StartCoroutine(Flicker(_screen, false));
        }

        _playerMask = LayerMask.GetMask("Player");
        AudioManager.Instance.PlayResource(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_canMove == false)
        {
            return;
        }
        //TODO: if selected connect the input otherwise disable it
        float signedAngle = Vector2.SignedAngle(_movementVector, Vector2.up) + _northYRotation; //offset this by the rotation
        
        Quaternion desiredQuat = Quaternion.Euler(0, signedAngle, 0);
        if (transform.rotation.y > desiredQuat.y + 0.02 || transform.rotation.y < desiredQuat.y - 0.02) //if not in the tight range of target angle, keep rotating
        {
            var step = _rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredQuat, step);
        } else //if looking in the right direction and canmove, move forward
        {
            _rb.AddForce(transform.forward * _movementSpeed);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _canMove = true;
        if (SelectionManager.Instance.GetCurrentSmartObject().gameObject.CompareTag("Player") == false)
        {
            _canMove = false;
            return;
        }

        
        if (ctx.canceled)
        {
            _canMove = false;
        }

        _movementVector = ctx.ReadValue<Vector2>();        
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (InventoryManager.Instance.HasThrowable() == false) { return; }

        if (ctx.performed)
        {
            ShootThrowable();
        }

    }

    /// <summary>
    /// Gets throwable from stack and shoots it out from player with collisions accounted for
    /// </summary>
    private void ShootThrowable()
    {
        if (InventoryManager.Instance.HasThrowable() == false)
        {
            return;
        }

        // Pop throwable from stack then throw it
        Collectable throwable = InventoryManager.Instance.PopThrowable();
        throwable.GetComponent<Rigidbody>().excludeLayers = _playerMask;
        AudioManager.Instance.PlayResource(8);
        throwable.transform.position = _launchPoint.transform.position;
        throwable.GetComponent<Rigidbody>().AddForce(transform.forward * _shootForce, ForceMode.Impulse);

        // Reset collisions and visual UI
        StartCoroutine(WaitABit(throwable));
    }

    /// <summary>
    /// Reenables gravity, colliders, and updates inventory UI
    /// </summary>
    /// <param name="thrw"></param>
    /// <returns></returns>
    private IEnumerator WaitABit(Collectable thrw)
    {
        thrw.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(.5f);
        thrw.GetComponent<Rigidbody>().excludeLayers = 0;
        InventoryManager.Instance.UpdateVisuals();
    }

    /// <summary>
    /// Turns on and off the screen object at random intervals indefinitely
    /// </summary>
    /// <param name="screen"></param>
    /// <param name="even"></param>
    /// <returns></returns>
    IEnumerator Flicker(GameObject screen, bool even)
    {
        if (screen.activeInHierarchy)
        {
            screen.SetActive(false);
        } else
        {
            screen.SetActive(true);
        }
        float delay;
        if (even)
        {
            delay = Random.Range(0.1f, 5f);
        } else
        {
            delay = Random.Range(0.1f, 0.3f);
        }
            
        yield return new WaitForSeconds(delay);
        _flickerCoroutine = StartCoroutine(Flicker(screen, !even));
    }

    public void GiveBatteryToRobot()
    {
        SetBatteryTrue();
        AudioManager.Instance.PlayResource(2);
        _electricityFX.SetActive(true);
        StopCoroutine(_flickerCoroutine);
        _screen.SetActive(true);
        StartCoroutine(ShowFullBattery());
    }

    public void SetBatteryTrue()
    {
        _hasBattery = true;
    }

    public void SetBandAidTrue()
    {
        _hasBandAid = true;
    }

    IEnumerator ShowFullBattery()
    {
        if (_animator != null)
        {
            _animator.Play("FullBat");
        }
        yield return new WaitForSeconds(2.5f);
        if (_animator != null)
        {
            if (_hasBandAid)
            {
                _animator.Play("Happy");
                _smokeMovementFX.SetActive(true);
            }
            else
            {
                _animator.Play("Idle");
            }
                
        }
    }

    public void GiveBandaidToRobot()
    {
        if (_brokenModel != null && _fixedModel != null)
        {
            AudioManager.Instance.PlayResource(1);
            _brokenModel.SetActive(false);
            _fixedModel.SetActive(true);
            //_smokeBurstFX.SetActive(true);
            _smokeBurstFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            _smokeBurstFX.Play();
            if (_hasBandAid && _hasBattery)
            {
                _animator.Play("Happy");
                _smokeMovementFX.SetActive(true);
            }
        }
    }
    
    public void OnCollisionEnter(Collision collision)
    {
        float speed = _rb.linearVelocity.magnitude;
        //Debug.Log("speed = " + speed);
        //Debug.Log("min = " + minImpactSpeed);
        
        if (speed < minImpactSpeed)
        {
            //Debug.Log("NotFastEnough");
            return;
        }
        
        //Debug.Log("Entered");
        _animator.Play("Closed_Eyes");
        _recentHurt = true;
        
        ContactPoint contactPoint = collision.GetContact(0);
        Quaternion rotation = Quaternion.LookRotation(contactPoint.normal);
        GameObject vfx = Instantiate(_smokeCollideFX, contactPoint.point, rotation);
        vfx.SetActive(true);
        Destroy(vfx.gameObject, 2f);

/*
        if (collision.gameObject.CompareTag(requiredTag)){
            _smokeBurstFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            _smokeBurstFX.Play();
        }
        */

    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(requiredTag)){
            _smokeBurstFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            _smokeBurstFX.Play();
        }
    }

    
    public void OnCollisionExit(Collision collision)
    {
        if(_recentHurt == true && _hasBandAid && _hasBattery)
        {
            _animator.Play("Idle");
        }
        else if(_recentHurt == true)
        {
            _animator.Play("Sad");
        }

    }

}
