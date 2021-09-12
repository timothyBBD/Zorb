using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPickup : MonoBehaviour
{
    PlayerController playerController;
    GameObject player;

    public FavorType favors = FavorType.Stength;
    public float favorAmount = 0f;
    public float health;
    public AudioSource audioSource;
    public AudioClip pickUpSound;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        audioSource = player.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerCollider")
        {
            if (favors == FavorType.Agility)
            {
                playerController.IncreaseAgility(favorAmount);
            }
            else
            {
                playerController.IncreaseStrength(favorAmount);
            }
            playerController.TakeDamage(-health);
            audioSource.PlayOneShot(pickUpSound);
            Destroy(gameObject);
        }
    }
}
