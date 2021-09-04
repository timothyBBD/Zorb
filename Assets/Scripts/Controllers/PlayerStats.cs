

public class PlayerStatsController
{
    public float strength;
    private int agility;
    private int health;
    private int MAX_STAT = 100;
    private int MIN_STAT = 0;

    public void IncreaseAgility(int agilityIncrease)
    {
        if (agility + agilityIncrease <= MAX_STAT)
        {
            agility += agilityIncrease;
            strength -= agilityIncrease;
        }
    }

    public void IncreaseStrength(int strengthIncrease)
    {
        if (strength + strengthIncrease <= MAX_STAT)
        {
            strength += strengthIncrease;
            agility -= strengthIncrease;
        }
    }

    public void IncreaseHealth(int healthIncrease)
    {
        if (health + healthIncrease <= MAX_STAT)
        {
            health += healthIncrease;
        }
    }

    public void DecreaseHealth(int healthDecrease)
    {
        if (health - healthDecrease >= MIN_STAT)
        {
            health -= healthDecrease;
        }
    }
}