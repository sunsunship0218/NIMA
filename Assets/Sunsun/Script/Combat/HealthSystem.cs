
using System;
using UnityEngine;

public class HealthSystem 
{
   float health;
public  float MaxHealth;
    //��׭�
   float postureAmount;
    float postureAmountMax;
    public bool IsInvunerable;
   public event Action OnTakeDamage;
   public event EventHandler OnHealthChange;
    public event EventHandler OnPostureChange;
   public  event Action OnDie;
    public event Action OnStagger;

    //��l�Ʀ�q����ҭ�
    public HealthSystem(float Maxhealth, float maxPosture)
    {
        //�̤j��q,�̤j����
       this.MaxHealth = Maxhealth;
       this.postureAmountMax = maxPosture;
        //�{�b��q
        health = Maxhealth;
        postureAmount = 0;
    }
  
    public float GetHealth()
    {
       return health;
    }
    public float GetMaxHealth()
    {
        return MaxHealth;
    }
    public float GetPostureAmount()
    {
        return postureAmount;
    }

    public float GetPostureAmountMax()
    {
        return postureAmountMax;
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
        if (OnHealthChange != null)
        {
            OnHealthChange(this, EventArgs.Empty);
        }

    }
    //�L��
    public  void SetInvunerable(bool isInvunerable)
    {
        this.IsInvunerable = isInvunerable;     
    }
    //�W�[��׭�
    //�p�G��׭Ⱥ��F,�I�s�B�z���ƥ�
    public void PostureIncrese(float amount)
    {
        postureAmount = Mathf.Min(postureAmount + amount, postureAmountMax);
        OnPostureChange?.Invoke(this, EventArgs.Empty);

        if (postureAmount == postureAmountMax)
        {

            OnStagger?.Invoke();
        }
    }
    //��֮���
    public  void PostureDecrease(float amount)
    {
        postureAmount = Mathf.Max(postureAmount - amount, 0);
        OnPostureChange?.Invoke(this, EventArgs.Empty);
    }
    //���]���
    public void SetPostureDefault()
    {
        postureAmount = 0;
    }
}
