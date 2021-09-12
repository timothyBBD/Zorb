using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float damage;
    public Sprite destroySprite;

    SpriteRenderer spriteRenderer;
    float fadeSpeed = 1.5f;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Obstacles")
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
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
