using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    Enemystate ActualenemyState = new Enemystate();

    private void Update()
    {
        switch (ActualenemyState)
        {
            case Enemystate.Idle:
                Debug.Log("Idle");
                break;
            case Enemystate.Walk:
                Debug.Log("Walk");
                break;
            case Enemystate.Run:
                Debug.Log("Run");
                break;
            case Enemystate.Dodge:
                Debug.Log("Dodge");
                break;
            case Enemystate.Attack:
                Debug.Log("Attack");
                break;

        }
    }

}
public enum Enemystate
{
    Idle,
    Walk,
    Run,
    Dodge,
    Attack
}
