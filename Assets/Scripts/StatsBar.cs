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

    public float maxStrength;
    public float maxAgility;
    public float maxHealth;

    private float health;
    private float strength;
    private float agility;

    public float Health
    {
        get => health;
        set
        {
            health = value;
            fillHealthBar();
        }
    }
    public float Strength { 
        get => strength;
        set
        {
            strength = value;
            fillStrengthBar();
        }
    }
    public float Agility { 
        get => agility;
        set
        {
            agility = value;
            fillAgilityBar();
        }
    }

    private int RoundUp(int toRound)
    {
        if (toRound % 10 == 0) return toRound;
        return (10 - toRound % 10) + toRound;
    }

    private void fillHealthBar()
    {
        float healthInterval = RoundUp((int)Health);

        int healthImageArray = (int)healthInterval / 10;

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
        fillRoundedBar(agilityBar, agilityRoundedBar, Strength, maxStrength);
    }

    private void fillAgilityBar()
    {
        fillRoundedBar(strengthBar, strengthRoundedBar, Agility, maxAgility);
    }

    public void initializeStats(float health, float agility, float strength, float maxHealth, float maxStrength, float maxAgility)
    {
        Health = health;
        Strength = strength;
        Agility = agility;
        this.maxStrength = maxStrength;
        this.maxAgility = maxAgility;
        this.maxHealth = maxHealth;

        fillAgilityBar();
        fillStrengthBar();
        fillHealthBar();
    }
}
