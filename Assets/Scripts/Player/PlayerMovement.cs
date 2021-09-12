using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    public Transform rotationAxis;
    public Animator animator;

    public float dashRate = 0.5f;
    public float dashDuration = 0.3f;
    public Camera mainCam;
    public GameObject dashTrail;
    public float rotation;
    private AudioSource audioSource;
    public AudioClip dashSound;
    private float dashCountdown = 0f;
    public bool isDashing = false;
    private Vector2 preDashVelocity;
    private PlayerController playerController;

    private PlayerControls controls;


    //Stats 
    private Stat MovementSpeed;
    private Stat DashSpeed;
    private Stat DashDuration;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Dash.performed += _ => Dash();
        controls.Player.Look.performed += ctx => RotatePlayer(ctx);

        controls.Enable();
    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        playerController = gameObject.GetComponent<PlayerController>();
        DashSpeed = playerController.getStat(StatType.DashSpeed);
        MovementSpeed = playerController.getStat(StatType.MovementSpeed);
        DashDuration = playerController.getStat(StatType.DashTime);

    }

    private void OnDisable()
    {
        controls.Disable();
    }



    private void RotatePlayer(InputAction.CallbackContext ctx)
    {
        Vector2 lookVector = ctx.ReadValue<Vector2>();

        Vector2 playerPos = mainCam.WorldToScreenPoint(transform.position);
        Vector3 rotateVector = lookVector - playerPos;
        rotation = Mathf.Atan2(rotateVector.y, rotateVector.x) * Mathf.Rad2Deg;
        rotationAxis.rotation = Quaternion.Euler(0, 0, rotation);
        animator.SetFloat("angle", rotation);
    }

    public void Update()
    {
        if (playerController.isDead)
        {
            dashCountdown = 0;
            EndDash();
            rb.velocity = Vector2.zero;
            return;
        }

        DashCooldown();
        EndDash();
        Move();
    }

    public void Move()
    {
        if (isDashing)
            return;
        rb.velocity = movementVector * MovementSpeed.currentValue * Time.fixedDeltaTime;
        animator.SetFloat("velocity", rb.velocity.magnitude);
    }

    public void MoveInput(InputAction.CallbackContext ctx)
    {
        movementVector = ctx.ReadValue<Vector2>();
    }

    private void DashCooldown()
    {
        dashCountdown -= Time.deltaTime;

    }
    private void EndDash()
    {

        if (dashCountdown < (dashRate - DashDuration.currentValue))
        {
            isDashing = false;
            rb.velocity = preDashVelocity;
            dashTrail.SetActive(false);
        }

    }


    private void Dash()
    {
        if (dashCountdown > 0 || playerController.isDead)
        {
            return;
        }
        preDashVelocity = rb.velocity;
        rb.velocity = rb.velocity + movementVector * DashSpeed.currentValue;
        isDashing = true;
        dashCountdown = dashRate;
        dashTrail.SetActive(true);
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(dashSound);

    }

}
