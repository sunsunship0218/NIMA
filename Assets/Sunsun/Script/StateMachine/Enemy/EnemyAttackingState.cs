using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VFX;

public class EnemyAttackingState : EnemyBaseState
{
    const float TransitionDuration = 0.15f;
    //攻擊時間的紀錄
    float lastAttackTime = 0f;
    float AttackCoolTIme = 1f;
    //連擊
    const float ComboResetTime = 1.0f;
    int currentComboStep = 0;
    const int maxComboSteps = 6;
    //
    bool alreadyAppliedForce;
    float previousFrameTime;

    Attack Shortattack;
    Attack MidAttack;
    Attack LongAttack;
    //存放攻擊的陣列
    readonly int[] ShortRangeAttackHashes;
    readonly int[] MidRangeAttackHashes;
    readonly int[] LongRangeAttackHashes;

    private readonly Dictionary<AttackRange, int[]> attackAnimations = new Dictionary<AttackRange, int[]>
{
    { AttackRange.Short, new int[] { Animator.StringToHash("ShortAttack1"), Animator.StringToHash("ShortAttack2") } },
    { AttackRange.Mid, new int[] { Animator.StringToHash("MidAttack1"), Animator.StringToHash("MidAttack2") } },
    { AttackRange.Long, new int[] { Animator.StringToHash("LongAttack1") } }
};
    public enum AttackRange
    {
        Short,
        Mid,
        Long
    }
    public EnemyAttackingState(EnemyStateMachine enemyStateMachine, int attackIndex) : base(enemyStateMachine)
    {
        
     }


    public override void Enter()
    {
        // 開始新的連擊
        //doComboAttacks(currentComboStep);


    }
    public override void Update(float deltaTime)
    {


        // 更新角色控制器以確保位置更新
        MoveWithDeltatime(deltaTime);
        //朝向腳色攻擊
        FacePlayer();

        // 獲取當前動畫的進行時間
        float normalizedTime = GetNormalizedTime(enemyStatemachine.animator);

        // 獲取動畫完成度
        AnimatorStateInfo currentStateInfo = enemyStatemachine.animator.GetCurrentAnimatorStateInfo(0);
        bool isAttackAnimationFinished = currentStateInfo.normalizedTime >= 0.95f &&
            (currentStateInfo.IsName("Attack1") || currentStateInfo.IsName("Attack2") || currentStateInfo.IsName("Attack3")
            || currentStateInfo.IsName("Attack4") || currentStateInfo.IsName("Attack5") || currentStateInfo.IsName("Attack6")
            );

        // 检查是否需要施加力
        /*if (normalizedTime >= attack.ForceTime && !alreadyAppliedForce)
        {
            // 施加向前的力
            enemyStatemachine.forceReceiver.AddForce(enemyStatemachine.transform.forward * attack.Force);
            alreadyAppliedForce = true;
        }*/
        // 更新 previousFrameTime
        previousFrameTime = normalizedTime;

        if (!IsinAttackingRange() || !IsInCirclingRange())
        {
            enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
            return;
        }
        //完成動畫撥放,切換狀態
        if (isAttackAnimationFinished)
        {
            //檢查攻擊狀態冷卻
            if (Time.time - lastAttackTime > AttackCoolTIme)
            {
                //這裡要替換成距離
                if (IsinAttackingRange())
                {
                    //連擊還沒結束
                    if (currentComboStep < maxComboSteps - 1)
                    {
                        currentComboStep++;
                        //doComboAttacks(currentComboStep);
                    }
                    else
                    {
                        // 連擊結束後，檢查是否需要追逐
                        if (IsInChasingRange() && !IsinAttackingRange())
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

        }
    }
    public override void Exit()
    {


    }
    AttackRange GetAttackRange(float distanceToPlayer)
    {
        if (distanceToPlayer <= 2.0f) return AttackRange.Short;
        if (distanceToPlayer <= 5.0f) return AttackRange.Mid;
        return AttackRange.Long;
    }
    /*  void doComboAttacks(int comboStep)
      {
          if(comboStep>=0 && comboStep< maxComboSteps)
          {
              int selectedAttackHash = AttackHashes[comboStep];
              // 攻擊傷害判定
              enemyStatemachine.weaponDamageL.SetAttack(attack.Damage, attack.knockbackRange);
              enemyStatemachine.weaponDamageR.SetAttack(attack.Damage, attack.knockbackRange);

              // 播放選定的攻擊動畫
            enemyStatemachine.animator.CrossFadeInFixedTime(selectedAttackHash, TransitionDuration, 0);
              lastAttackTime = Time.time; // 更新最後攻擊時間
          }

      }
    */
}
