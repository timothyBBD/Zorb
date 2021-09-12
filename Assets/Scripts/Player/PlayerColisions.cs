using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColisions : MonoBehaviour
{
    string GunPartTag = "GunPart";

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject collidedObject = other.gameObject;
        string collisionTag = other.gameObject.tag;
        if(collisionTag == GunPartTag){
            int partId = collidedObject.GetComponent<GunPart>().id;
            GameState.PartsCollected[partId] = true;
        }
    }
}
