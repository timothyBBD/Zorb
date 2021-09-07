using System;
using UnityEngine;

public class EnemyBulletProjectile : MonoBehaviour
{
    PlayerController player;
    string playerTag = "Player";
    string[] collisionTags = new string[] { "Player" };

    public float bulletDamage = 5f;
    public float bulletSpeed = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        Debug.Log(collisionTag);
        if (Array.Exists(collisionTags, tag => tag == collisionTag))
        {
            if(collisionTag == playerTag)
            {
                player.TakeDamage(bulletDamage);
            }
            Destroy(gameObject);
        }
    }

}
