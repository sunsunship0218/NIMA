using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VFX;

public class EnemyAttackingState : EnemyBaseState
{
    //攻擊動畫存放的陣列
    readonly int[] AttackHashes = new int[]
    {
        Animator.StringToHash("Attack1"),
        Animator.StringToHash("Attack2"),
        Animator.StringToHash("Attack3")
    };

    const float TransitionDuration = 0.1f;
    //攻擊時間的紀錄
    float lastAttackTime = 0f;
    float AttackCoolTIme = 0.5f;
    //連擊
    const float ComboResetTime = 1.0f;
    int currentComboStep = 0;
    const int maxComboSteps = 3;

    public EnemyAttackingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    Attack attack;
    public override void Enter()
    {
        //避免被卡楨的時間影響
        lastAttackTime = Time.time;
        // 開始新的連擊
        currentComboStep = 0;
        // 隨機進行單個攻擊
        /*
        int randomIndex ;
        do
        {
            randomIndex = Random.Range(0, AttackHashes.Length);
        } while (randomIndex == lastAttackIndex);
        lastAttackIndex = randomIndex;
        int selectedAttackHash = AttackHashes[randomIndex];
        // 播放选定的攻击动画
        enemyStatemachine.animator.CrossFadeInFixedTime(selectedAttackHash, TransitionDuration, 0);
          //攻擊傷害判定
        enemyStatemachine.weaponDamageL.SetAttack(enemyStatemachine.AttackingDamage, enemyStatemachine.KnockBack);
        //攻擊傷害判定
        enemyStatemachine.weaponDamageR.SetAttack(enemyStatemachine.AttackingDamage, enemyStatemachine.KnockBack);
        */

        doComboAttacks(currentComboStep);


    }
    public override void Update(float deltaTime)
    {      
        // 檢查攻擊動畫是否完成
        AnimatorStateInfo currentStateInfo = enemyStatemachine.animator.GetCurrentAnimatorStateInfo(0);
        bool isAttackAnimationFinished = currentStateInfo.normalizedTime >= 0.8f &&
            (currentStateInfo.IsName("Attack1") || currentStateInfo.IsName("Attack2") || currentStateInfo.IsName("Attack3"));

       
     
        //完成動畫撥放,切換狀態
        if (isAttackAnimationFinished)
        {
            //檢查攻擊狀態冷卻
            if (Time.time - lastAttackTime > AttackCoolTIme)
            {
                if (IsinAttackingRange())
                {
                    if (currentComboStep < maxComboSteps - 1)
                    {
                        currentComboStep++;
                        doComboAttacks(currentComboStep);
                    }
                    else
                    {
                        // 連擊結束，重置連擊步驟並重新開始,讓機率決定要做什麼
                        enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
                    }
                }
                else if (enemyStatemachine.target != null)
                {
                    enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
                }
                else
                {
                    enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine));
                }
            }
        }
        

    }
    public override void Exit()
    {
        Debug.Log("Exist Attacking state");
        
    }

    void doComboAttacks(int comboStep)
    {
        if(comboStep>=0 && comboStep< maxComboSteps)
        {
            int selectedAttackHash = AttackHashes[comboStep];

            // 攻擊傷害判定
            enemyStatemachine.weaponDamageL.SetAttack(enemyStatemachine.AttackingDamage, enemyStatemachine.KnockBack);
            enemyStatemachine.weaponDamageR.SetAttack(enemyStatemachine.AttackingDamage, enemyStatemachine.KnockBack);

            // 播放選定的攻擊動畫
            enemyStatemachine.animator.CrossFadeInFixedTime(selectedAttackHash, TransitionDuration, 0);

            lastAttackTime = Time.time; // 更新最後攻擊時間
        }

    }
 }
