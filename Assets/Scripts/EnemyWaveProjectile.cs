using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveProjectile : EnemyBulletProjectile
{
    public Vector3 targetScale = Vector3.one;
    public Vector3 startingScale = Vector3.zero;
    public float scaleSpeed = 2.0f;

    void Start()
    {
        transform.localScale = startingScale;
    }

    void FixedUpdate()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
    }

}
