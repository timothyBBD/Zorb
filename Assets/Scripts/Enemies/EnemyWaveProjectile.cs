using System;
using UnityEngine;

public class EnemyWaveProjectile : EnemyBulletProjectile
{
    PlayerController player;
    Animator projectileAnimator;
    string playerTag = "Player";
    string[] collisionTags = new string[] { "PlayerCollider" };

    public Vector3 targetScale = Vector3.one;
    public Vector3 startingScale = Vector3.zero;
    public float scaleSpeed = 2.0f;

    void Awake()
    {
        projectileAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerController>();
        transform.localScale = startingScale;
    }

    void FixedUpdate()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.fixedDeltaTime);
        bool closeEnough = Vector3.Dot(transform.localScale, targetScale) > 0.99f;
        if(closeEnough && !projectileAnimator.GetBool("FadeOut"))
        {
            projectileAnimator.SetBool("FadeOut", true);
        }
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
        }
    }

}
