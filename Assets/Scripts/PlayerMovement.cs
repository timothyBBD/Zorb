using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private int initialHealth = 100;
    private int initialAgility = 50;
    private int initialStrength = 50;


    private Vector2 movementVector;
    public float movementSpeed;
    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 velocity = new Vector3(movementVector.x, 0, movementVector.y) * movementSpeed * Time.fixedDeltaTime;
        rb.velocity = velocity;
    }

    public void MoveInput(InputAction.CallbackContext ctx)
    {
        movementVector = ctx.ReadValue<Vector2>();
    }


}
