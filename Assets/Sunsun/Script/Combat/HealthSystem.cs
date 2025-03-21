
using System;
using UnityEngine;

public class HealthSystem 
{
   float health;
public  float MaxHealth;
    //格擋值
   float postureAmount;
    float postureAmountMax;
    public bool IsInvunerable;
   public event Action OnTakeDamage;
   public event EventHandler OnHealthChange;
    public event EventHandler OnPostureChange;
   public  event Action OnDie;
    public event Action OnStagger;

    //初始化血量跟格黨值
    public HealthSystem(float Maxhealth, float maxPosture)
    {
        //最大血量,最大格黨
       this.MaxHealth = Maxhealth;
       this.postureAmountMax = maxPosture;
        //現在血量
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
        if (OnHealthChange != null)
        {
            OnHealthChange(this, EventArgs.Empty);
        }

    }
    //無傷
    public  void SetInvunerable(bool isInvunerable)
    {
        this.IsInvunerable = isInvunerable;     
    }
    //增加格擋值
    //如果格擋值滿了,呼叫處理的事件
    public void PostureIncrese(float amount)
    {
        postureAmount = Mathf.Min(postureAmount + amount, postureAmountMax);
        OnPostureChange?.Invoke(this, EventArgs.Empty);

        if (postureAmount == postureAmountMax)
        {

            OnStagger?.Invoke();
        }
    }
    //減少格黨
    public  void PostureDecrease(float amount)
    {
        postureAmount = Mathf.Max(postureAmount - amount, 0);
        OnPostureChange?.Invoke(this, EventArgs.Empty);
    }
    //重設格擋
    public void SetPostureDefault()
    {
        postureAmount = 0;
    }
}
