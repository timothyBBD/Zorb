using System;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Image healthBar;
    public Image agilityBar;
    public Image strengthBar;
    public Image agilityRoundedBar;
    public Image strengthRoundedBar;

    public Sprite[] healthStates;

    private float ROUNDED_BAR_PERCENTAGE = 1 / 3f;
    private float STRAIGHT_BAR_PERCENTAGE = 2 / 3f;

    private float maxStrength = 100f;
    private float maxAgility = 100f;
    private float maxHealth = 100f;

    private float health = 90f;
    private float strength = 100f;
    private float agility = 100f;

    private void Update()
    {
        health--;
        fillHealthBar();
    }

    private void fillHealthBar()
    {
        int healthInterval = (int)health - (int)health % 10;
        int healthImageArray = (healthInterval / 10) + 1;

        if (healthImageArray < 0) { return; }

        healthBar.GetComponent<Image>().sprite = healthStates[healthImageArray];
    }

    private void fillRoundedBar(Image straightBar, Image roundedBar, float stat, float maxStat)
    {
        float fillPercentage = stat / maxStat;
        float amountInRounded = fillPercentage < ROUNDED_BAR_PERCENTAGE ? fillPercentage : ROUNDED_BAR_PERCENTAGE; ;
        float amountInStraight = fillPercentage - amountInRounded;

        roundedBar.fillAmount = amountInRounded / ROUNDED_BAR_PERCENTAGE;
        straightBar.fillAmount = amountInStraight / STRAIGHT_BAR_PERCENTAGE;
    }

    private void fillStrengthBar()
    {
        fillRoundedBar(agilityBar, agilityRoundedBar, strength, maxStrength);
    }

    private void fillAgilityBar()
    {
        fillRoundedBar(strengthBar, strengthRoundedBar, agility, maxAgility);
    }

    public void increaseStrength(float amountToIncrease)
    {
        if (amountToIncrease + strength >= maxStrength)
        {
            strength += amountToIncrease;
            agility -= amountToIncrease;
            fillStrengthBar();
            fillAgilityBar();
        }
        
    }

    public void increaseAgility(float amountToIncrease)
    {
        if (amountToIncrease + agility >= maxAgility)
        {
            agility += amountToIncrease;
            strength -= amountToIncrease;
            fillStrengthBar();
            fillAgilityBar();
        }
    }

    public void increaseHealth(float amountToIncrease)
    {
        if (amountToIncrease + health > maxHealth)
        {
            health += amountToIncrease;
            fillHealthBar();
        }
    }

    public void decreaseHealth(float amountToDecrease)
    {
        health -= amountToDecrease;
        fillHealthBar();

        if (health <= 0)
        {
            throw new NotImplementedException("Do death");
        }
    }

    public void initializeStats(float health, float agility, float strength,float maxHealth, float maxStrength, float maxAgility)
    {
        this.health = health;
        this.strength = strength;
        this.agility = agility;
        this.maxStrength = maxStrength;
        this.maxAgility = maxAgility;
        this.maxHealth = maxHealth;
    }
}
