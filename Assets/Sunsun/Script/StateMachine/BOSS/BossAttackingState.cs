using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackingState : BossBaseState
{
    const float TransitionDuration = 0.15f;
    //�����ɶ�������
    float lastAttackTime = 0f;
    float AttackCoolTIme = 1f;
    //�s��
    const float ComboResetTime = 1.0f;
    int currentComboStep = 0;
    //Phase1
    readonly int maxShortComboSteps=0;
    readonly int maxMidComboSteps=0;
    readonly int maxLongComboSteps=0;
    //Phase2
    readonly int S2maxShortComboSteps = 0;
    readonly int S2maxMidComboSteps = 0;
    readonly int S2maxLongComboSteps = 0;

    bool alreadyAppliedForce;
    float previousFrameTime;
    //Phas1
    Attack ShortAttack;
    Attack MidAttack;
    Attack LongAttack;
    //Phase2
    Attack S2_ShortAttack;
    Attack S2_MidAttack;
    Attack S2_LongAttack;
    //���q�����}�C
    //�s��������}�C
    //Phase1
    readonly int[] ShortRangeAttackHashes;
    readonly int[] MidRangeAttackHashes;
    readonly int[] LongRangeAttackHashes;
    //phase2
    readonly int[] S2_ShortRangeAttackHashes;
    readonly int[] S2_MidRangeAttackHashes;
    readonly int[] S2_LongRangeAttackHashes;
    public BossAttackingState(BossStateMachine enemyStateMachine, int attackIndex) : base(enemyStateMachine)
    {
        //�u�Z��
        ShortAttack = enemyStatemachine.ShortAttacks[attackIndex];
        //�ʵeinitial hash name
        ShortRangeAttackHashes = new int[enemyStatemachine.ShortAttacks.Length];
        for (int i = 0; i < enemyStatemachine.ShortAttacks.Length; i++)
        {
            ShortRangeAttackHashes[i] = Animator.StringToHash(enemyStateMachine.ShortAttacks[i].AnimationName);
        }

        //���Z��
        MidAttack = enemyStatemachine.MidAttacks[attackIndex];
        //�ʵeinitial hash name
        MidRangeAttackHashes = new int[enemyStatemachine.MidAttacks.Length];
        for (int i = 0; i < enemyStatemachine.MidAttacks.Length; i++)
        {
            MidRangeAttackHashes[i] = Animator.StringToHash(enemyStateMachine.MidAttacks[i].AnimationName);
        }

        //���Z��
        LongAttack = enemyStatemachine.LongAttacks[attackIndex];
        //�ʵeinitial hash name
        LongRangeAttackHashes = new int[enemyStatemachine.LongAttacks.Length];
        for (int i = 0; i < enemyStatemachine.LongAttacks.Length; i++)
        {
            LongRangeAttackHashes[i] = Animator.StringToHash(enemyStateMachine.LongAttacks[i].AnimationName);
        }

        //�U�Z���̤j��
        maxShortComboSteps = enemyStatemachine.ShortAttacks.Length;
        maxMidComboSteps = enemyStatemachine.MidAttacks.Length;
        maxLongComboSteps = enemyStatemachine.LongAttacks.Length;
        //------------------------------------------------------------------------
        //Phase 2
        //�u�Z��
        S2_ShortAttack = enemyStatemachine.S2_ShortAttacks[attackIndex];
        //�ʵeinitial hash name
        S2_ShortRangeAttackHashes = new int[enemyStatemachine.S2_ShortAttacks.Length];
        for (int i = 0; i < enemyStatemachine.S2_ShortAttacks.Length; i++)
        {
            S2_ShortRangeAttackHashes[i] = Animator.StringToHash(enemyStateMachine.S2_ShortAttacks[i].AnimationName);
        }

        //���Z��
        S2_MidAttack = enemyStatemachine.S2_MidAttacks[attackIndex];
        //�ʵeinitial hash name
        S2_MidRangeAttackHashes = new int[enemyStatemachine.S2_MidAttacks.Length];
        for (int i = 0; i < enemyStatemachine.S2_MidAttacks.Length; i++)
        {
            S2_MidRangeAttackHashes[i] = Animator.StringToHash(enemyStateMachine.S2_MidAttacks[i].AnimationName);
        }

        //���Z��
        S2_MidAttack = enemyStatemachine.S2_MidAttacks[attackIndex];
        //�ʵeinitial hash name
        S2_MidRangeAttackHashes = new int[enemyStatemachine.S2_MidAttacks.Length];
        for (int i = 0; i < enemyStatemachine.S2_LongAttacks.Length; i++)
        {
            S2_ShortRangeAttackHashes[i] = Animator.StringToHash(enemyStateMachine.S2_ShortAttacks[i].AnimationName);
        }

        //�U�Z���̤j��
        S2maxShortComboSteps = enemyStatemachine.S2_ShortAttacks.Length;
        S2maxMidComboSteps = enemyStatemachine.S2_MidAttacks.Length;
        S2maxLongComboSteps = enemyStatemachine.S2_LongAttacks.Length;
    }


    public override void Enter()
    {
        Debug.Log("enter boss atk");
        if (enemyStatemachine.inPhase2 == false)
        {
            Phase1ATK();
        }
        else if (enemyStatemachine.inPhase2 == true)
        {
            Phase2ATK();
        }
      


    }
    public override void Update(float deltaTime)
    {

        // ��s���ⱱ�agent�H�T�O��m��s
        MoveWithDeltatime(deltaTime);
        //�¦V�}�����
        FacePlayer();

        // �����e�ʵe���i��ɶ�
        float normalizedTime = GetNormalizedTime(enemyStatemachine.animator, "Attack");

        // ����ʵe������
        int attackTagHash = Animator.StringToHash("Attack");
        AnimatorStateInfo currentStateInfo = enemyStatemachine.animator.GetCurrentAnimatorStateInfo(0);
        bool isAttackAnimationFinished = currentStateInfo.normalizedTime >= 0.95f && currentStateInfo.tagHash == attackTagHash;



      if (normalizedTime >= ShortAttack.ForceTime && !alreadyAppliedForce)
        {
            // �I�[�V�e���O
            enemyStatemachine.forceReceiver.AddForce(enemyStatemachine.transform.forward * ShortAttack.Force);
            alreadyAppliedForce = true;
        }
        if (normalizedTime >= MidAttack.ForceTime && !alreadyAppliedForce)
        {
            // �I�[�V�e���O
            enemyStatemachine.forceReceiver.AddForce(enemyStatemachine.transform.forward * MidAttack.Force);
            alreadyAppliedForce = true;
        }
        if (normalizedTime >= LongAttack.ForceTime && !alreadyAppliedForce)
        {
            enemyStatemachine.forceReceiver.AddForce(enemyStatemachine.transform.forward * LongAttack.Force);
            alreadyAppliedForce = true;
        }
        // ��s previousFrameTime
        previousFrameTime = normalizedTime;
  

    /*    if (!IsinAttackingRange() && !IsInCirclingRange())
        {
            enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
            return;
        }
    */
        //�����ʵe����,�������A
        if (isAttackAnimationFinished)
        {
            //�ˬd�������A�N�o
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
                //�o�̭n�������Z��
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
        else if (IsinLongAttackingRange())
        {
            //Debug.Log(" IN LONG RANGE ATTACK");
            doLongComboAttacks(currentComboStep, Time.deltaTime);
        }
    }
    void Phase2ATK()
    {
          if (IsinShortAttackingRange())
        {
            doShortComboAttacks(currentComboStep);
        }
        else if (IsinMidAttackRange())
        {
            doMidComboAttacks(currentComboStep);
        }
        else if (IsinLongAttackingRange())
        {
            //Debug.Log(" IN LONG RANGE ATTACK");
            doLongComboAttacks(currentComboStep, Time.deltaTime);
        }
    }
    //Phase1
    void doShortComboAttacks(int comboStep)
    {
        if (comboStep >= 0 && comboStep < maxShortComboSteps)
        {
            int selectedAttackHash = ShortRangeAttackHashes[comboStep];
            // �����ˮ`�P�w
            enemyStatemachine.weaponDamageL.SetAttack(ShortAttack.Damage, ShortAttack.knockbackRange);
            enemyStatemachine.weaponDamageR.SetAttack(ShortAttack.Damage, ShortAttack.knockbackRange);

            // �����w�������ʵe
            enemyStatemachine.animator.CrossFadeInFixedTime(selectedAttackHash, TransitionDuration, 0);
            lastAttackTime = Time.time; // ��s�̫�����ɶ�
        }

    }
    void doMidComboAttacks(int comboStep)
    {

        if (comboStep >= 0 && comboStep < maxMidComboSteps)
        {
            int selectedAttackHash = MidRangeAttackHashes[comboStep];
            // �����ˮ`�P�w
            enemyStatemachine.weaponDamageL.SetAttack(MidAttack.Damage, MidAttack.knockbackRange);
            enemyStatemachine.weaponDamageR.SetAttack(MidAttack.Damage, MidAttack.knockbackRange);

            // �����w�������ʵe
            enemyStatemachine.animator.CrossFadeInFixedTime(selectedAttackHash, TransitionDuration, 0);
            lastAttackTime = Time.time; // ��s�̫�����ɶ�
        }

    }
    void doLongComboAttacks(int comboStep, float deltatime)
    {


        if (comboStep >= 0 && comboStep < maxLongComboSteps)
        {
            int selectedAttackHash = LongRangeAttackHashes[comboStep];
            // �����ˮ`�P�w
            enemyStatemachine.weaponDamageL.SetAttack(LongAttack.Damage, LongAttack.knockbackRange);
            enemyStatemachine.weaponDamageR.SetAttack(LongAttack.Damage, LongAttack.knockbackRange);
            //
            enemyStatemachine.forceReceiver.AddForce(enemyStatemachine.transform.forward * LongAttack.Force);
            alreadyAppliedForce = true;


            // �����w�������ʵe
            enemyStatemachine.animator.CrossFadeInFixedTime(selectedAttackHash, TransitionDuration, 0);
            lastAttackTime = Time.time; // ��s�̫�����ɶ�
        }

    }
    //Phase2
    void S2_ShortComboAttacks(int comboStep)
    {
        if (comboStep >= 0 && comboStep < S2maxShortComboSteps)
        {
            int selectedAttackHash = S2_ShortRangeAttackHashes[comboStep];
            // �����ˮ`�P�w
            enemyStatemachine.weaponDamageL.SetAttack(S2_ShortAttack.Damage, S2_ShortAttack.knockbackRange);
            enemyStatemachine.weaponDamageR.SetAttack(S2_ShortAttack.Damage, S2_ShortAttack.knockbackRange);

            // �����w�������ʵe
            enemyStatemachine.animator.CrossFadeInFixedTime(selectedAttackHash, TransitionDuration, 0);
            lastAttackTime = Time.time; // ��s�̫�����ɶ�
        }

    }
    void S2_doMidComboAttacks(int comboStep)
    {

        if (comboStep >= 0 && comboStep <S2maxMidComboSteps)
        {
            int selectedAttackHash = S2_MidRangeAttackHashes[comboStep];
            // �����ˮ`�P�w
            enemyStatemachine.weaponDamageL.SetAttack(S2_MidAttack.Damage, S2_MidAttack.knockbackRange);
            enemyStatemachine.weaponDamageR.SetAttack(S2_MidAttack.Damage, S2_MidAttack.knockbackRange);

            // �����w�������ʵe
            enemyStatemachine.animator.CrossFadeInFixedTime(selectedAttackHash, TransitionDuration, 0);
            lastAttackTime = Time.time; // ��s�̫�����ɶ�
        }

    }
    void S2_doLongComboAttacks(int comboStep, float deltatime)
    {


        if (comboStep >= 0 && comboStep < S2maxLongComboSteps)
        {
            int selectedAttackHash = S2_LongRangeAttackHashes[comboStep];
            // �����ˮ`�P�w
            enemyStatemachine.weaponDamageL.SetAttack(S2_LongAttack.Damage, S2_LongAttack.knockbackRange);
            enemyStatemachine.weaponDamageR.SetAttack(S2_LongAttack.Damage, S2_LongAttack.knockbackRange);
            //
            enemyStatemachine.forceReceiver.AddForce(enemyStatemachine.transform.forward * S2_LongAttack.Force);
            alreadyAppliedForce = true;


            // �����w�������ʵe
            enemyStatemachine.animator.CrossFadeInFixedTime(selectedAttackHash, TransitionDuration, 0);
            lastAttackTime = Time.time; // ��s�̫�����ɶ�
        }

    }
    void TransitionAfterCombo()
    {
        // �s��������A�ˬd�O�_�ݭn�l�v
     /*if (IsInChasingRange() && !IsinAttackingRange())
        {
            enemyStatemachine.SwitchState(new BossIdleState(enemyStatemachine));
        }
      */
    }
}
