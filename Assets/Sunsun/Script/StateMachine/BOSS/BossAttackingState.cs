using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackingState : BossBaseState
{
    const float TransitionDuration = 0.15f;
    //攻擊時間的紀錄
    float lastAttackTime = 0f;
    float AttackCoolTIme = 1f;
    //連擊
    const float ComboResetTime = 1.0f;
    int currentComboStep = 0;
    readonly int maxShortComboSteps;
    readonly int maxMidComboSteps;
    readonly int maxLongComboSteps;

    //
    bool alreadyAppliedForce;
    float previousFrameTime;

    Attack ShortAttack;
    Attack MidAttack;
    Attack LongAttack;
    //階段攻擊陣列
    //存放攻擊的陣列
    readonly int[] ShortRangeAttackHashes;
    readonly int[] MidRangeAttackHashes;
    readonly int[] LongRangeAttackHashes;

    public BossAttackingState(BossStateMachine enemyStateMachine, int attackIndex) : base(enemyStateMachine)
    {
        //短距離
        ShortAttack = enemyStatemachine.ShortAttacks[attackIndex];
        //動畫initial hash name
        ShortRangeAttackHashes = new int[enemyStatemachine.ShortAttacks.Length];
        for (int i = 0; i < enemyStatemachine.ShortAttacks.Length; i++)
        {
            ShortRangeAttackHashes[i] = Animator.StringToHash(enemyStateMachine.ShortAttacks[i].AnimationName);
        }

        //中距離
        MidAttack = enemyStatemachine.MidAttacks[attackIndex];
        //動畫initial hash name
        MidRangeAttackHashes = new int[enemyStatemachine.MidAttacks.Length];
        for (int i = 0; i < enemyStatemachine.MidAttacks.Length; i++)
        {
            MidRangeAttackHashes[i] = Animator.StringToHash(enemyStateMachine.MidAttacks[i].AnimationName);
        }

        //長距離
        LongAttack = enemyStatemachine.LongAttacks[attackIndex];
        //動畫initial hash name
        LongRangeAttackHashes = new int[enemyStatemachine.LongAttacks.Length];
        for (int i = 0; i < enemyStatemachine.LongAttacks.Length; i++)
        {
            LongRangeAttackHashes[i] = Animator.StringToHash(enemyStateMachine.LongAttacks[i].AnimationName);
        }

        //各距離最大的
        maxShortComboSteps = enemyStatemachine.ShortAttacks.Length;
        maxMidComboSteps = enemyStatemachine.MidAttacks.Length;
        maxLongComboSteps = enemyStatemachine.LongAttacks.Length;
    }


    public override void Enter()
    {
        if (enemyStatemachine.inPhase2 == false)
        {
            Phase1ATK();
        }
        else if (enemyStatemachine.inPhase2 == true)
        {

        }
      


    }
    public override void Update(float deltaTime)
    {

        // 更新角色控制器agent以確保位置更新
        MoveWithDeltatime(deltaTime);
        //朝向腳色攻擊
        FacePlayer();

        // 獲取當前動畫的進行時間
        float normalizedTime = GetNormalizedTime(enemyStatemachine.animator, "Attack");

        // 獲取動畫完成度
        AnimatorStateInfo currentStateInfo = enemyStatemachine.animator.GetCurrentAnimatorStateInfo(0);
        bool isAttackAnimationFinished = currentStateInfo.normalizedTime >= 0.95f &&
            (currentStateInfo.IsName("Attack1") || currentStateInfo.IsName("Attack2") || currentStateInfo.IsName("Attack3")
            || currentStateInfo.IsName("Attack4") || currentStateInfo.IsName("Attack5") || currentStateInfo.IsName("Attack6")
            || currentStateInfo.IsName("Attack7")
            );



      if (normalizedTime >= ShortAttack.ForceTime && !alreadyAppliedForce)
        {
            // 施加向前的力
            enemyStatemachine.forceReceiver.AddForce(enemyStatemachine.transform.forward * ShortAttack.Force);
            alreadyAppliedForce = true;
        }
        if (normalizedTime >= MidAttack.ForceTime && !alreadyAppliedForce)
        {
            // 施加向前的力
            enemyStatemachine.forceReceiver.AddForce(enemyStatemachine.transform.forward * MidAttack.Force);
            alreadyAppliedForce = true;
        }
        if (normalizedTime >= LongAttack.ForceTime && !alreadyAppliedForce)
        {
            enemyStatemachine.forceReceiver.AddForce(enemyStatemachine.transform.forward * LongAttack.Force);
            alreadyAppliedForce = true;
        }
        // 更新 previousFrameTime
        previousFrameTime = normalizedTime;
  

    /*    if (!IsinAttackingRange() && !IsInCirclingRange())
        {
            enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
            return;
        }
    */
        //完成動畫撥放,切換狀態
        if (isAttackAnimationFinished)
        {
            //檢查攻擊狀態冷卻
            if (Time.time - lastAttackTime > AttackCoolTIme)
            {
                currentComboStep++;
                if (IsinLongAttackingRange() && currentComboStep > maxLongComboSteps)
                {
                    currentComboStep = 0;
                }
                if (IsinMidAttackRange() && currentComboStep > maxMidComboSteps)
                {
                    currentComboStep = 0;
                }
                if (IsinShortAttackingRange() && currentComboStep > maxShortComboSteps)
                {
                    currentComboStep = 0;
                }
                //這裡要替換成距離
                if (IsinShortAttackingRange())
                {
                    doShortComboAttacks(currentComboStep);
               /*     float chance = Random.Range(0, 3);
                    if (chance > 1.75)
                    {
                        enemyStatemachine.SwitchState(new EnemyCirclingState(enemyStatemachine));
                    }
                    else
                    {
                      
                    }
               */
                }
                else if (IsinMidAttackRange())
                {
                    doMidComboAttacks(currentComboStep);
                }
                else if (IsinLongAttackingRange())
                {
                    doLongComboAttacks(currentComboStep, deltaTime);
                }
            }
            else
            {
                TransitionAfterCombo();
            }


        }
    }
    public override void Exit()
    {
        Debug.Log("EXIT BOSS ATK");
    }

    void Phase1ATK()
    {
        if (IsinShortAttackingRange())
        {
            doShortComboAttacks(currentComboStep);
        }
        else if (IsinMidAttackRange())
        {
            doMidComboAttacks(currentComboStep);
        }
        else
        {
            //Debug.Log(" IN LONG RANGE ATTACK");
            doLongComboAttacks(currentComboStep, Time.deltaTime);
        }
    }
    void Phase2ATK()
    {
        Debug.Log("Phase2 ATK");
    }
    void doShortComboAttacks(int comboStep)
    {
        if (comboStep >= 0 && comboStep < maxShortComboSteps)
        {
            int selectedAttackHash = ShortRangeAttackHashes[comboStep];
            // 攻擊傷害判定
            enemyStatemachine.weaponDamageL.SetAttack(ShortAttack.Damage, ShortAttack.knockbackRange);
            enemyStatemachine.weaponDamageR.SetAttack(ShortAttack.Damage, ShortAttack.knockbackRange);

            // 播放選定的攻擊動畫
            enemyStatemachine.animator.CrossFadeInFixedTime(selectedAttackHash, TransitionDuration, 0);
            lastAttackTime = Time.time; // 更新最後攻擊時間
        }

    }
    void doMidComboAttacks(int comboStep)
    {

        if (comboStep >= 0 && comboStep < maxMidComboSteps)
        {
            int selectedAttackHash = MidRangeAttackHashes[comboStep];
            // 攻擊傷害判定
            enemyStatemachine.weaponDamageL.SetAttack(MidAttack.Damage, MidAttack.knockbackRange);
            enemyStatemachine.weaponDamageR.SetAttack(MidAttack.Damage, MidAttack.knockbackRange);

            // 播放選定的攻擊動畫
            enemyStatemachine.animator.CrossFadeInFixedTime(selectedAttackHash, TransitionDuration, 0);
            lastAttackTime = Time.time; // 更新最後攻擊時間
        }

    }

    void doLongComboAttacks(int comboStep, float deltatime)
    {


        if (comboStep >= 0 && comboStep < maxLongComboSteps)
        {
            int selectedAttackHash = LongRangeAttackHashes[comboStep];
            // 攻擊傷害判定
            enemyStatemachine.weaponDamageL.SetAttack(LongAttack.Damage, LongAttack.knockbackRange);
            enemyStatemachine.weaponDamageR.SetAttack(LongAttack.Damage, LongAttack.knockbackRange);
            //
            enemyStatemachine.forceReceiver.AddForce(enemyStatemachine.transform.forward * LongAttack.Force);
            alreadyAppliedForce = true;


            // 播放選定的攻擊動畫
            enemyStatemachine.animator.CrossFadeInFixedTime(selectedAttackHash, TransitionDuration, 0);
            lastAttackTime = Time.time; // 更新最後攻擊時間
        }

    }

    void TransitionAfterCombo()
    {
        // 連擊結束後，檢查是否需要追逐
     /*if (IsInChasingRange() && !IsinAttackingRange())
        {
            enemyStatemachine.SwitchState(new BossIdleState(enemyStatemachine));
        }
      */
    }
}
