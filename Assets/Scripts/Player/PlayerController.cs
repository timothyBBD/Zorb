using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float strength = 100f;
    public float agility = 100f;
    public float health = 100f;
    public float MAX_STAT = 100f;
    public float MIN_STAT = 0f;
    public StatsBar statsBar;

    public float MaxDamageReduction = 70f;

    public void Start()
    {
        statsBar.initializeStats(health, agility,strength, MAX_STAT, MAX_STAT, MAX_STAT);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject.name);
        if (collider.gameObject.tag == "EnemyAttack")
        {
            switch (collider.gameObject.name)
            {
                case "EnemyBullet(Clone)":
                    float damage = collider.gameObject.GetComponent<EnemyBulletProjectile>().bulletDamage;
                    TakeDamage(damage);
                    break;
                default:
                    break;
            }
        }
        
    }

    public void IncreaseAgility(float agilityIncrease)
    {
        if (agility + agilityIncrease <= MAX_STAT)
        {
            agility += agilityIncrease;
            strength -= agilityIncrease;
            statsBar.Agility = agility;
            statsBar.Strength = strength;
        }
    }

    public void IncreaseStrength(float strengthIncrease)
    {
        if (strength + strengthIncrease <= MAX_STAT)
        {
            strength += strengthIncrease;
            agility -= strengthIncrease;
            statsBar.Agility = agility;
            statsBar.Strength = strength;
        }
    }

    public void TakeDamage(float amount)
    {
        float damageReduced = amount * ((MaxDamageReduction/100) * (strength / MAX_STAT));
        float actualDamage = amount - damageReduced;

        health -= actualDamage;

        statsBar.Health = health;

        Debug.Log(health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
