using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementVector;
    public float movementSpeed;
    private Rigidbody2D rb;
    public Transform rotationAxis;
    public Animator animator;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = movementVector * movementSpeed * Time.fixedDeltaTime;
        animator.SetFloat("velocity", rb.velocity.magnitude);
    }
    public void RotatePlayer()
    {
        float rotation = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;
        rotationAxis.rotation = Quaternion.Euler(0, 0, rotation);
        animator.SetFloat("angle", rotation);
    }

    public void MoveInput(InputAction.CallbackContext ctx)
    {
        movementVector = ctx.ReadValue<Vector2>();
        RotatePlayer();
    }
}
