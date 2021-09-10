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

    public float dashSpeed;
    public float dashRate = 0.5f;
    public float dashDuration = 0.3f;

    private float dashCountdown = 0f;
    private bool isDashing = false;
    private Vector2 preDashVelocity;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Dash.performed += _ => Dash();


        controls.Enable();
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        DashCooldown();
        EndDash();
        Move();
    }

    public void Move()
    {
        if (isDashing)
            return;
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

    private void DashCooldown()
    {
        dashCountdown -= Time.deltaTime;

    }
    private void EndDash()
    {

        if (dashCountdown < (dashRate - dashDuration))
        {
            isDashing = false;
            rb.velocity = preDashVelocity;
        }

    }


    private void Dash()
    {
        if (dashCountdown > 0)
        {
            return;
        }
        preDashVelocity = rb.velocity;
        rb.velocity = rb.velocity + movementVector * dashSpeed;
        isDashing = true;
        dashCountdown = dashRate;

    }

}
