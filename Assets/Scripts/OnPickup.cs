using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPickup : MonoBehaviour
{
    string playerTag = "Player";
    PlayerController playerController;
    GameObject player;

    public enum FavorType
    {
        Stength,
        Agility,
    }

    public FavorType favors = FavorType.Stength;
    public float favorAmount = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        playerController = player.GetComponent<PlayerController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals(playerTag))
        {
            if(favors == FavorType.Agility)
            {
                playerController.IncreaseAgility(favorAmount);
            } else
            {
                playerController.IncreaseStrength(favorAmount);
            }
            Destroy(gameObject);
        }
    }
}
