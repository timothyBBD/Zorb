using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletProjectile : MonoBehaviour
{
    public float bulletDamage = 5f;
    public float bulletSpeed = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

}
