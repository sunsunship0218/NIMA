using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public HealthSystem healthSystem;
  public bool IsBoss = false;
    private PlayerStateMachine playerStateMachine;
    void Awake()
    {
        // ��쪱�a���A���A�`�N�o�̰��]�������u���@�� PlayerStateMachine
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        healthSystem = new HealthSystem(100, 0);
        if(this.gameObject.name == "Boss")
        {
            healthSystem = new HealthSystem(200, 0);
        }
    }
  


    private void OnEnable()
    {
        if (playerStateMachine != null && !playerStateMachine.EnemyList.Contains(gameObject))
        {
            playerStateMachine.EnemyList.Add(gameObject);
        }
    }

    private void OnDisable()
    {
        if (playerStateMachine != null && playerStateMachine.EnemyList.Contains(gameObject))
        {
            playerStateMachine.EnemyList.Remove(gameObject);
        }
    }

}
