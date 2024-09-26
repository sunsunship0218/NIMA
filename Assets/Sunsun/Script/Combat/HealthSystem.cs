
using System;

public class HealthSystem 
{
   float health;
   float MaxHealth;
    public bool IsInvunerable;
   public event Action OnTakeDamage;
   public event EventHandler OnHealthChange;
   public event Action OnDie;

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
        if (health == 0)   { return; }
        //格黨跟防禦成功,無傷
        if (IsInvunerable) { return; }
        health = Math.Max(health - damageAmount, 0);
        OnTakeDamage?.Invoke();
        //血量變化的event
        if (OnHealthChange != null)
         {
             OnHealthChange(this, EventArgs.Empty);
         }
        //血量變化如果到0,死
        if(health == 0)
        {
            OnDie?.Invoke();
        }
        /*
         *   health -= damageAmount;
          if (health < 0)
          {
              health = 0;
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

    //無傷
    public  void SetInvunerable(bool isInvunerable)
    {
        this.IsInvunerable = isInvunerable;     
    }
}
