using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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


[Serializable]
public class HealthBarState
{
    public float fillPercentageMin;
    public float fillPercentageMax;
    public float healthMin;
    public float healthMax;
    public Image stateImage;

    public void SetHealth(float health)
    {
        if (health < healthMin)
        {
            health = healthMin;
        }
        if (health > healthMax)
        {
            health = healthMax;
        }
        float healthDiff = healthMax - healthMin;
        float healthPercentage = (health - healthMin) / healthDiff;
        stateImage.fillAmount = Mathf.Lerp(fillPercentageMin, fillPercentageMax, healthPercentage);

    }
}

public class PlayerController : MonoBehaviour
{
    public float strength;
    public float agility;
    public float health;
    // UI Elements
    public TMP_Text armourText;

    public Image agilityBar;
    public Image strengthBar;
    public List<HealthBarState> healthBarStates;
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
        agilityBar.fillAmount = agility / 100f;
        strengthBar.fillAmount = strength / 100f;
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
        armourText.text = getStat(StatType.DamageReduction).currentValue.ToString() + "%";
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
        healthBarStates.ForEach(state => state.SetHealth(health));
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
