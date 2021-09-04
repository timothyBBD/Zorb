using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementVector;
    public float movementSpeed;
    private Rigidbody2D rb;

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
    }

    public void MoveInput(InputAction.CallbackContext ctx)
    {
        movementVector = ctx.ReadValue<Vector2>();
    }


}
