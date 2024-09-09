
using System;

public class HealthSystem 
{
   float health;
   float MaxHealth;
    public event EventHandler onHealthChange;

    //初始化血量
    public HealthSystem(float Maxhealth)
    {
        //最大血量
       this.MaxHealth = Maxhealth;
        //現在血量
        health= Maxhealth;
    }
  
    public float GetHealth()
    {
       return health;
    }
    //返回現在血量
    public float ReturnHealth( )
    {
        return (float)health;
    }
    //造成傷害
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

    //治療
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
