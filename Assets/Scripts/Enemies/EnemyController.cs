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

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        pathfinding = GetComponent<AIPath>();
        enemyAI = GetComponent<EnemyAI>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerBullet")
        {
            var damage = collider.gameObject.GetComponent<PlayerBullet>().damage;
            TakeDamage(damage);
            Destroy(collider.gameObject);
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            pathfinding.enabled = false;
            enemyAI.enabled = false;
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


}
