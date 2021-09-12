using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGunPart : MonoBehaviour
{
    public int gunPartId = 0;

    public GunPart gunPart;

    void Awake()
    {   
        gunPart.id = gunPartId;
    }

    void Update()
    {
        if (transform.childCount == 0)
        {
            Instantiate(gunPart, transform.parent.transform.position, Quaternion.identity);
            Destroy(this);
        }
    }
}
