using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float strength;
    public float agility;
    public float health;
    public float MAX_STAT = 100f;
    public float MIN_STAT = 0f;

    public float MaxDamageReduction = 70f;

    public void IncreaseAgility(float agilityIncrease)
    {
        if (agility + agilityIncrease <= MAX_STAT)
        {
            agility += agilityIncrease;
            strength -= agilityIncrease;
        }
    }

    public void IncreaseStrength(float strengthIncrease)
    {
        if (strength + strengthIncrease <= MAX_STAT)
        {
            strength += strengthIncrease;
            agility -= strengthIncrease;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount - (amount * MaxDamageReduction * strength / MAX_STAT);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
