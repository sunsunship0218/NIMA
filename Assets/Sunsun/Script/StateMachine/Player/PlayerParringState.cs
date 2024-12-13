using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParringState : PlayerBaseState
{
    readonly int ParryHash = Animator.StringToHash("Parry");
    const float duration = 0.14f;
    float minBlockTime = 0.2f;
    float timer = 0f;

    public PlayerParringState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }
    public override void Enter()
    {
        timer = 0f;
        playerStateMachine.playerHealth.healthSystem.SetInvunerable(true);
        playerStateMachine.animator.CrossFadeInFixedTime(ParryHash, duration);

    }
    public override void Update(float deltaTime)
    {
        MoveWithDeltatime(deltaTime);
        //�w�Įɶ�
        timer += deltaTime;
        //����ʵe���񪬺A,����S�����񧹴N�������A
        AnimatorStateInfo currentStateInfo = playerStateMachine.animator.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.IsName("Parry") && currentStateInfo.normalizedTime < 0.8f)
        {
            return;
        }
        //����A���Ƥ���
        if (timer < minBlockTime)
        {
            return;
        }

        if (!playerStateMachine.playerInputHandler.isParrying)
        {
            playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
            return;
        }
        Debug.Log(playerStateMachine.playerInputHandler.isParrying);
        //�p�G�]�m�F�o�ӱ���,�b�S���ؼЪ��ɭ�,�ʵe�|���P����
        /* if (playerStateMachine.targeter.currentTarget == null)
         {
             playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
             return;
         }*/
    }
    public override void Exit()
    {

            playerStateMachine.playerHealth.healthSystem.SetInvunerable(false);
    }
}
