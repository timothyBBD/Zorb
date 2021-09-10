using System;
using UnityEngine;

public class EnemyBulletProjectile : MonoBehaviour
{
    protected PlayerController player;
    string playerTag = "Player";
    protected string[] collisionTags = new string[] { "PlayerCollider" };

    public float bulletDamage = 5f;
    public float bulletSpeed = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (Array.Exists(collisionTags, tag => tag == collisionTag))
        {
            if (collisionTag == "PlayerCollider")
            {
                player.TakeDamage(bulletDamage);
            }
            Destroy(gameObject);
        }
    }

}
