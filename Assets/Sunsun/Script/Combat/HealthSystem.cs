
using System;

public class HealthSystem 
{
   float health;
   float MaxHealth;
    public bool IsInvunerable;
   public event Action OnTakeDamage;
   public event EventHandler OnHealthChange;
   public event Action OnDie;

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
        if (health == 0)   { return; }
        //���Ҹ򨾿m���\,�L��
        if (IsInvunerable) { return; }
        health = Math.Max(health - damageAmount, 0);
        OnTakeDamage?.Invoke();
        //��q�ܤƪ�event
        if (OnHealthChange != null)
         {
             OnHealthChange(this, EventArgs.Empty);
         }
        //��q�ܤƦp�G��0,��
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

    //�L��
    public  void SetInvunerable(bool isInvunerable)
    {
        this.IsInvunerable = isInvunerable;     
    }
}
