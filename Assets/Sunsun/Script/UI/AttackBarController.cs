using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AttackBarController : MonoBehaviour
{
    float attackBarValue = 0f;         // 當前攻擊條數值
    float maxAttackBarValue = 100f;      // 攻擊條滿值的設定
     float increaseAmount = 20f;          // 每次擊中敵人增加的數值（可根據需求調整）
     float decayRate = 10f;               // 每秒衰減的數值量

    // 控制是否允許累積攻擊條數值
    // 預設可以累積，當累積到滿值後會設定成 false，
    // 再等值衰減到 0 時重新開放累積。
    bool canAccumulate = true;
    private float healTimer = 0f;
    bool isBarFull = false;
    // 定義當攻擊條滿值時要觸發的事件
    public static event Action OnAttackBarFull;

   [SerializeField] PlayerHealth playerHealth;
    [SerializeField] PlayerStateMachine playerStateMachine;
    [SerializeField] Slider attackSilder;


    private void Update()
    {
        // 如果目前不允許累積（即進入冷卻狀態）且攻擊條數值尚未歸零，就持續衰減
        if (!canAccumulate && attackBarValue > 0f)
        {
            attackBarValue -= decayRate * Time.deltaTime;
            if (attackBarValue < 0f)
                attackBarValue = 0f;
        }

        // 當攻擊條完全衰減為 0 時，恢復累積
        if (attackBarValue == 0f)
        {
            canAccumulate = true;
            isBarFull = false;
        }

        // 每幀更新 Slider 的顯示
        if (attackSilder != null)
            attackSilder.value = attackBarValue / maxAttackBarValue;
    }
  

    void HandleEnemyHit()
    {
        // 如果目前允許累積，就執行累加邏輯
        if (canAccumulate && playerStateMachine.playerInputHandler.GetIsAttacking()==true)
        {
        //    Debug.Log("Increasing attack bar");
            attackBarValue += increaseAmount;
            if (attackBarValue >= maxAttackBarValue)
            {
                attackBarValue = maxAttackBarValue;
                isBarFull = true;
                // 累積到滿值後，立即禁止累積，等待完全衰退至 0
                canAccumulate = false;
                // 觸發攻擊條滿值事件
                OnAttackBarFull?.Invoke();
                if (attackSilder != null)
                    attackSilder.value = 1f;  // 顯示滿
            }
            else
            {
                // 累加後更新 UI
                if (attackSilder != null)
                    attackSilder.value = attackBarValue / maxAttackBarValue;
            }
        }
        else
        {
            // 在冷卻期間（不可累積時），即使攻擊碰撞發生也不增加攻擊條數值
            // 但如果需要在此期間仍觸發回血（例如條件滿足時），可以保留或調整以下邏輯：
            if (playerHealth != null && attackBarValue > 0f)
            {
                playerHealth.healthSystem.HealAmount(10);
           //     Debug.Log("Player healed during cooldown");
            }
        }
    }

    private void OnEnable()
    {
      
        WeaponDamage.OnEnemyHit +=HandleEnemyHit;
    }

    private void OnDisable()
    {
      
        WeaponDamage.OnEnemyHit -= HandleEnemyHit;
    }

}
