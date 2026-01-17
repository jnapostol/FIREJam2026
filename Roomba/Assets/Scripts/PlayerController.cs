using MoreMountains.Feedbacks;
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

    Rigidbody _rb;

    bool _canMove = true;
    Coroutine _canMoveCoroutine;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _canMove = false;
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
        Debug.Log($"movement vector: {_movementVector}");


        _movementVector = ctx.ReadValue<Vector2>();        
    }

    private void OnEnable()
    {
        EnableInput();
    }
    private void OnDisable()
    {
        DisableInput();
    }

    public void EnableInput()
    {

    }

    public void DisableInput()
    {

    }
}
