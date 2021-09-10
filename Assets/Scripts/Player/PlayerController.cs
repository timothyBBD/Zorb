using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public enum FavorType
{
    Stength,
    Agility,
}

public enum StatType
{
    MovementSpeed, FireRate, DashSpeed, ShotSpeed, DashTime, DamageReduction, ShotDamage, ShotSize
}

[Serializable]
public class Stat
{
    public StatType statType;
    public FavorType favorType;
    public float minValue;
    public float maxValue;
    public float currentValue;


}

public class PlayerController : MonoBehaviour
{
    public float strength;
    public float agility;
    public float health;
    public GameObject healthSliderObj;
    private Slider healthSlider;

    [SerializeField] private List<Stat> affectedStats;
    const float MAX_PERCENTAGE = 100f;
    public bool isDead;
    public Animator animator;
    private PlayerMovement playerMovement;


    private void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        UpdateStats();
    }

    private void UpdateStats()
    {
        affectedStats.ForEach(stat =>
        {
            if (stat.favorType == FavorType.Stength)
            {
                stat.currentValue = Mathf.Lerp(stat.minValue, stat.maxValue, strength / MAX_PERCENTAGE);
            }
            else
            {
                stat.currentValue = Mathf.Lerp(stat.minValue, stat.maxValue, agility / MAX_PERCENTAGE);
            }
        });
    }


    public void IncreaseAgility(float agilityIncrease)
    {
        if (agility + agilityIncrease <= MAX_PERCENTAGE)
        {
            agility += agilityIncrease;
            strength -= agilityIncrease;
        }
        UpdateStats();

    }

    public void IncreaseStrength(float strengthIncrease)
    {
        if (strength + strengthIncrease <= MAX_PERCENTAGE)
        {
            strength += strengthIncrease;
            agility -= strengthIncrease;
        }
        UpdateStats();
    }
    public Stat getStat(StatType statType)
    {
        return affectedStats.Find(stat => stat.statType == statType);
    }

    public void TakeDamage(float amount)
    {
        if (playerMovement.isDashing)
            return;
        var damageReduction = getStat(StatType.DamageReduction);
        health -= amount - (amount * ((damageReduction.currentValue / 100f) * (strength / MAX_PERCENTAGE)));
        if (health <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
