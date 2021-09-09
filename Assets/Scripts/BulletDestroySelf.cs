using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroySelf : MonoBehaviour
{
    public float timeToLive = 5f;

    private void Start()
    {
        StartCoroutine("DestroySelf");
    }
    IEnumerator DestroySelf()
    {
        yield return new WaitForSecondsRealtime(timeToLive);
        Destroy(gameObject);
    }
}
