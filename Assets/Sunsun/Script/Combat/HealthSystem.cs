
using System;

public class HealthSystem 
{
   float health;
   float MaxHealth;
    public event EventHandler onHealthChange;

    //��l�Ʀ�q
    public HealthSystem(float Maxhealth)
    {
        //�̤j��q
       this.MaxHealth = Maxhealth;
        //�{�b��q
        health= Maxhealth;
    }
  
    public float GetHealth()
    {
       return health;
    }
    //��^�{�b��q
    public float ReturnHealth( )
    {
        return (float)health;
    }
    //�y���ˮ`
    public void Damage(float damageAmount)
    {
        if (health == 0) { return; }
        health = Math.Max(health - damageAmount, 0);

        /*
         *   health -= damageAmount;
          if (health < 0)
          {
              health = 0;
          }
         */
      /*  if (onHealthChange != null)
        {
            onHealthChange(this, EventArgs.Empty);
        }
      */
    }

    //�v��
    public void HealAmount(int healAmount)
    {
        health += healAmount;
        if (health > MaxHealth)
        {
            health = MaxHealth;
        }
        /*     if (onHealthChange != null)
             {
                 onHealthChange(this, EventArgs.Empty);
             }
        */
    }

}
