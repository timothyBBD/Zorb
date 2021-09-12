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
    private PlayerController playerController;
    private AudioSource audioSource;
    public AudioClip shootSound;


    //Stats 
    private Stat ShotDamage;
    private Stat FireRate;
    private Stat ShotSpeed;
    private Stat ShotSize;

    private void Awake()
    {
        controls = new PlayerControls();
        audioSource = GetComponent<AudioSource>();
        controls.Player.Fire.performed += _ => canFire = !canFire;
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        ShotDamage = playerController.getStat(StatType.ShotDamage);
        ShotSpeed = playerController.getStat(StatType.ShotSpeed);
        ShotSize = playerController.getStat(StatType.ShotSize);
        FireRate = playerController.getStat(StatType.FireRate);
    }

    private void Update()
    {
        if (playerController.isDead)
            return;
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
        Vector3 direction = shootAxis.TransformDirection(shootAxis.right);
        Debug.Log(direction);
        float rotation = gameObject.GetComponent<PlayerMovement>().rotation;
        GameObject shot = Instantiate<GameObject>(playerBulletPrefab, shootPoint.position, Quaternion.Euler(0, 0, rotation)) as GameObject;
        Rigidbody2D rbShot = shot.GetComponent<Rigidbody2D>();
        shot.GetComponent<PlayerBullet>().damage = ShotDamage.currentValue;
        shot.transform.localScale = new Vector3(ShotSize.currentValue, ShotSize.currentValue, 1);
        rbShot.velocity = shootAxis.right * ShotSpeed.currentValue;
        fireRateCountdown = FireRate.currentValue;
        shootParticles.Play();
        audioSource.PlayOneShot(shootSound);
        shootAnimator.SetTrigger("shoot");
        shootAnimator.SetBool("isShooting", true);
    }
}