using System;
using System.Collections;
using UnityEngine;

public class EnemyWaveProjectile : EnemyBulletProjectile
{
    Animator projectileAnimator;

    PlayerMovement playerMovement;

    public Vector3 targetScale = Vector3.one;
    public Vector3 startingScale = Vector3.zero;
    public float scaleSpeed = 2.0f;

    void Awake()
    {
        projectileAnimator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        transform.localScale = startingScale;
        StartCoroutine(LerpFunction());
    }

    protected override void DestroyBullet()
    {
        // Do Nothing Intentionally
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

}
