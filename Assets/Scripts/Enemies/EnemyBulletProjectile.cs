using System;
using UnityEngine;

public class EnemyBulletProjectile : MonoBehaviour
{
    PlayerController player;
    PlayerMovement playerMovement;
    string playerTag = "Player";

    public float bulletDamage = 5f;
    public float bulletSpeed = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerController>();
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == "PlayerCollider" && !playerMovement.isDashing)
        {
            player.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }

        if (collisionTag == "Obstacles")
        {
            Destroy(gameObject);
        }
    }

}
