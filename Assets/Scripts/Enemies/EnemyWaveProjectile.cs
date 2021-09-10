using System;
using System.Collections;
using UnityEngine;

public class EnemyWaveProjectile : EnemyBulletProjectile
{
    Animator projectileAnimator;

    public Vector3 targetScale = Vector3.one;
    public Vector3 startingScale = Vector3.zero;
    public float scaleSpeed = 2.0f;

    void Awake()
    {
        projectileAnimator = GetComponent<Animator>();    
    }

    void Start()
    {
        transform.localScale = startingScale;
        StartCoroutine(LerpFunction());
    }

    IEnumerator LerpFunction()
    {
        float time = 0;

        while (time < scaleSpeed)
        {
            transform.localScale = Vector3.Lerp(startingScale, targetScale, time / scaleSpeed);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
        projectileAnimator.SetBool("FadeOut", true);
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
