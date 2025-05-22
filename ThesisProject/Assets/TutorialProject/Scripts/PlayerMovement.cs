using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    float moveSpeed;
    const float crouchSpeed = 100;
    const float runSpeed = 200;

    Vector3 movementInput;
    Vector3 movementVector;

    bool onGround;
    int jumpForce;

    void Start()
    {
        onGround = false;
        rb = GetComponent<Rigidbody>();
        moveSpeed = 150;
        jumpForce = 400;
    }

    void OnMovement(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        movementInput = new Vector3(inputVector.x, 0, inputVector.y);
    }

    void OnMovementStop(InputValue value)
    {
        movementInput = Vector3.zero;
    }

    void ApplyMovement()
    {
        if(onGround)
        {
            movementVector = (transform.right * movementInput.x) + (transform.forward * movementInput.z);
            movementVector.y = 0;
            rb.velocity = movementVector * Time.fixedDeltaTime * moveSpeed;
        }
    }

    void Jump()
    {
        if (onGround)
        {
            Vector3 jumpVector = Vector3.up * jumpForce;
            rb.AddForce(jumpVector);
            onGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onGround = true;
    }

    void CheckSpeed()
    {
        if(Keyboard.current.shiftKey.IsPressed())
        {
            moveSpeed = runSpeed;
        }
        else if(Keyboard.current.ctrlKey.IsPressed())
        {
            moveSpeed = crouchSpeed;
        }
        else
        {
            moveSpeed = 150;
        }
    }

    void Update()
    {
        CheckSpeed();
        ApplyMovement();
        if(Keyboard.current.spaceKey.IsPressed())
        {
            Jump();
        }
    }
}
