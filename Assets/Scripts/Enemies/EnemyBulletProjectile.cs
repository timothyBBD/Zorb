using System.Collections;
using UnityEngine;

public class EnemyBulletProjectile : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    PlayerController player;
    PlayerMovement playerMovement;
    string playerTag = "Player";
    float fadeSpeed = 1.5f;
    bool bulletDestroyed = false;

    public float bulletDamage = 5f;
    public float bulletSpeed = 3f;
    public Sprite destroySprite;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerController>();
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletDestroyed)
        {
            return;
        }
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == "PlayerCollider" && !playerMovement.isDashing)
        {
            player.TakeDamage(bulletDamage);
            DestroyBullet();
        }

        if (collisionTag == "Obstacles")
        {
            DestroyBullet();
        }
    }


    protected virtual void DestroyBullet()
    {

        GetComponent<Animator>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        spriteRenderer.sprite = destroySprite;
        StartCoroutine(FadeOutAndDestroy());
    }

    IEnumerator FadeOutAndDestroy()
    {
        while (spriteRenderer.color.a >= 0)
        {
            Color bulletColor = spriteRenderer.color;
            float fadeAmount = bulletColor.a - (fadeSpeed * Time.deltaTime);
            bulletColor = new Color(bulletColor.r, bulletColor.g, bulletColor.b, fadeAmount);
            spriteRenderer.color = bulletColor;
            yield return null;
        }
        Destroy(gameObject);
    }

}
