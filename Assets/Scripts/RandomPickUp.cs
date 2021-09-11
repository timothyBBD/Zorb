using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickUp : MonoBehaviour
{

    public List<GameObject> PickUpPrefabs;


    void Start()
    {
        int index = Mathf.RoundToInt(Random.Range(0, PickUpPrefabs.Count));
        GameObject pickUp = PickUpPrefabs[index];
        Instantiate(pickUp, transform.position, Quaternion.identity);
        Destroy(gameObject.GetComponent<SpriteRenderer>());
    }
}
