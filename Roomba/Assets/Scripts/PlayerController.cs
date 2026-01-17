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
    [SerializeField] float _rotateSpeed;
    [SerializeField] float _movementSpeed;
    [SerializeField] float _northYRotation;
    [SerializeField] float _shootForce;
    [SerializeField] Transform _launchPoint;

    Rigidbody _rb;

    bool _canMove = false;

    [Header("Broken State Stuff")]
    bool _isBroken = true;
    [SerializeField] GameObject _screen;
    [SerializeField] bool _hasBattery;
    [SerializeField] bool _hasBandAid;
    [SerializeField] GameObject _brokenModel;
    [SerializeField] GameObject _fixedModel;
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
        //TO DO: check gameManager's selectionmanager to get selected status

        _canMove = true;
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
        StartCoroutine(Flicker(screen, !even));
    }

    void FixRobot()
    {
        StopCoroutine(_flickerCoroutine);
        if (_brokenModel != null && _fixedModel != null)
        {
            _brokenModel.SetActive(false);
            _fixedModel.SetActive(true);

        }
    }
}
