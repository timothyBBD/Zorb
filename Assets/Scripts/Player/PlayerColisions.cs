using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColisions : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        string collisionTag = other.gameObject.tag;
        if(collisionTag.Contains("GunPart")){
            int partId = collisionTag[collisionTag.Length - 1];
            GameState.PartsCollected[partId] = true;
        }
    }
}
