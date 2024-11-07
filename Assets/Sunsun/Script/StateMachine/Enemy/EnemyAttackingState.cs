using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
 //   readonly int AttackHASH = Animator.StringToHash("Attack");
    const float TransitionDuration = 0.1f;
    int AttackCombo=0;
    float lastAttackTime = 0f;
    //上次攻擊
    int lastAttackIndex = -1;
    //攻擊動畫存放的陣列
    readonly int[] AttackHashes = new int[]
    {
        Animator.StringToHash("Attack1"),
        Animator.StringToHash("Attack2"),
        Animator.StringToHash("Attack3")
    };

    public EnemyAttackingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    Attack attack;
    public override void Enter()
    {
        //
        // 随机选择一个攻击动画
        int randomIndex ;
        do
        {
            randomIndex = Random.Range(0, AttackHashes.Length);
        } while (randomIndex == lastAttackIndex);
        lastAttackIndex = randomIndex;
        int selectedAttackHash = AttackHashes[randomIndex];

        //攻擊傷害判定
        enemyStatemachine.weaponDamageL.SetAttack(enemyStatemachine.AttackingDamage, enemyStatemachine.KnockBack);
        //攻擊傷害判定
        enemyStatemachine.weaponDamageR.SetAttack(enemyStatemachine.AttackingDamage, enemyStatemachine.KnockBack);
        //


        // 播放选定的攻击动画
        enemyStatemachine.animator.CrossFadeInFixedTime(selectedAttackHash, TransitionDuration, 0);
        //攻擊動畫撥放切換
        // enemyStatemachine.animator.CrossFadeInFixedTime(AttackHASH,TransitionDuration);


    }
    public override void Update(float deltaTime)
    {
        //完成動畫撥放,切換狀態
        AnimatorStateInfo currentStateInfo =enemyStatemachine.animator.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.normalizedTime >= 0.8 && currentStateInfo.IsName("Attack1") || currentStateInfo.IsName("Attack2") || currentStateInfo.IsName("Attack3"))
        {
                enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine));
        }
        if (IsinAttackingRange())
        {
            return;
        }
        else if (enemyStatemachine.target != null)
        {
            enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
        }
    }
    public override void Exit()
    {
        Debug.Log("Exist Attacking state");
    }
 }
