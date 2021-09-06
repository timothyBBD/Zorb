using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject playerBulletPrefab;
    public float fireRate = 0.5f;
    private float fireRateCountdown = 0f;
    private bool canFire = false;
    public Transform shootPoint;
    public Transform shootAxis;
    public float shootSpeed;
    public Camera mainCam;
    public ParticleSystem shootParticles;
    public Animator shootAnimator;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Fire.performed += _ => canFire = !canFire;


        controls.Enable();
    }

    private void Update()
    {
        TimeFireRate();
        FireInput();
    }

    private void TimeFireRate()
    {
        if (fireRateCountdown <= 0)
            return;

        fireRateCountdown -= Time.deltaTime;
    }
    public void FireInput()
    {
        if (fireRateCountdown > 0 || !canFire)
            return;

        GameObject shot = Instantiate<GameObject>(playerBulletPrefab, shootPoint.position, Quaternion.identity) as GameObject;
        Rigidbody2D rbShot = shot.GetComponent<Rigidbody2D>();
        rbShot.velocity = shootAxis.right * shootSpeed;
        fireRateCountdown = fireRate;
        shootParticles.Play();
        shootAnimator.SetTrigger("shoot");
        shootAnimator.SetBool("isShooting", true);
    }
}