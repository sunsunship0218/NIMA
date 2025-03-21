using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GamePadRumble : MonoBehaviour
{
    [SerializeField] float lowFrequency;
    [SerializeField] float highFrequency;
    [SerializeField] float duration;
    Gamepad gamepad;
    [SerializeField] playerInputHandler inputHandler;
    private void Awake()
    {

    }
    private void OnEnable()
    {
        WeaponDamage.OnEnemyHit += HandleHitRumble;
    }
    private void OnDisable()
    {
        WeaponDamage.OnEnemyHit -= HandleHitRumble;

    }

    private void HandleHitRumble()
    {
        gamepad = Gamepad.current;
        if (gamepad != null && inputHandler.isUsingPad) 
        {
            gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
            StartCoroutine(StopRumbleAfterDuration(duration,gamepad));
        }
    }
    IEnumerator StopRumbleAfterDuration(float duration, Gamepad pad)
    {
        float elapsedtime=0f;
       while (elapsedtime < duration)
        {
            elapsedtime+= Time.deltaTime;
            yield return null;
        }
        pad.SetMotorSpeeds(0f, 0f);
    }




}
