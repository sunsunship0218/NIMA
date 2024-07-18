
using System;

public class HealthSystem 
{
    int health;
    int MaxHealth;
    public event EventHandler onHealthChange;
    //現在血條剩多少
    public HealthSystem(int Maxhealth)
    {
        //最大血量
       this.MaxHealth = Maxhealth;
        //現在血量
        health= Maxhealth;
    }
  
    public int GetHealth()
    {
       return health;
    }
    public float GetHealthPerscent( )
    {
        return (float)health / MaxHealth;
    }
    //造成傷害
    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0)
        {
            health = 0;
        }
        if(onHealthChange != null)
        {
            onHealthChange(this, EventArgs.Empty);
        }
    }

    //治療
    public void HealAmount(int healAmount)
    {
        health += healAmount;
        if (health > MaxHealth)
        {
            health = MaxHealth;
        }
        if (onHealthChange != null)
        {
            onHealthChange(this, EventArgs.Empty);
        }
    }

}
