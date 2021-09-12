using Pathfinding;
using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float health;

    SpriteRenderer spriteRenderer;
    float fadeSpeed = 1f;
    Animator enemyAnimator;
    AIPath pathfinding;
    EnemyAI enemyAI;
    Color originalColor;
    HealthBar healthBar;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        enemyAnimator = GetComponent<Animator>();
        pathfinding = GetComponent<AIPath>();
        enemyAI = GetComponent<EnemyAI>();
    }

    void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(health);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerBullet")
        {
            var damage = collider.gameObject.GetComponent<PlayerBullet>().damage;
            TakeDamage(damage);
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void TakeDamage(float damage)
    {
        FlashRed();
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            Destroy(GetComponent<Collider2D>());
            Destroy(pathfinding);
            Destroy(enemyAI);
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
            enemyAnimator.SetBool("IsDead", true);
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    IEnumerator FadeOutAndDestroy()
    {
        while (spriteRenderer.color.a >= 0)
        {
            Color enemyColor = spriteRenderer.color;
            float fadeAmount = enemyColor.a - (fadeSpeed * Time.deltaTime);
            enemyColor = new Color(enemyColor.r, enemyColor.g, enemyColor.b, fadeAmount);
            spriteRenderer.color = enemyColor;
            yield return null;
        }
        Destroy(gameObject);
    }

    void FlashRed()
    {

        spriteRenderer.color = Color.red;
        Invoke("ResetColor", 0.1f);
    }
    void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }

}
