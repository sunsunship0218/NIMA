
using System;

public class HealthSystem 
{
    int health;
    int MaxHealth;
    public event EventHandler onHealthChange;
    //�{�b����Ѧh��
    public HealthSystem(int Maxhealth)
    {
        //�̤j��q
       this.MaxHealth = Maxhealth;
        //�{�b��q
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
    //�y���ˮ`
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

    //�v��
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
