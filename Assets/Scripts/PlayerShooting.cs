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



    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Fire.performed += _ => canFire = !canFire;

        controls.Enable();
    }


    private void Update()
    {
        RotateShootAxis();
        TimeFireRate();
        FireInput();
    }

    private void TimeFireRate()
    {
        if (fireRateCountdown <= 0)
            return;

        fireRateCountdown -= Time.deltaTime;
    }

    private void RotateShootAxis()
    {
        Vector2 mousePos = new Vector2(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue());
        Vector2 playerPos = mainCam.WorldToScreenPoint(transform.position);
        Vector3 rotateVector = mousePos - playerPos;
        float rotation = Mathf.Atan2(rotateVector.y, rotateVector.x) * Mathf.Rad2Deg;
        shootAxis.rotation = Quaternion.Euler(0, 0, rotation);
    }
    public void FireInput()
    {
        if (fireRateCountdown > 0 || !canFire)
            return;

        GameObject shot = Instantiate<GameObject>(playerBulletPrefab, shootPoint.position, Quaternion.identity) as GameObject;
        Rigidbody2D rbShot = shot.GetComponent<Rigidbody2D>();
        rbShot.velocity = shootAxis.right * shootSpeed;
        fireRateCountdown = fireRate;
    }
}