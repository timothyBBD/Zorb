using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float timeToLive = 3f;
    void Start()
    {
    }
    IEnumerator DestroySelf() 
    {
        yield return new WaitForSeconds(timeToLive); 
        Destroy(gameObject);
    }

}
