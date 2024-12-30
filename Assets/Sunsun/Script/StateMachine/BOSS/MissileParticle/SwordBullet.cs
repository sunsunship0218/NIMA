using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//子彈產生的位置
public class SwordBullet : MonoBehaviour
{
    public GameObject particleprefab;
    [SerializeField] BossStateMachine stateMachine;
    Vector2 direction;
    //產生子彈
    //在特效呼叫這個程式
    //gameobject 是預設位置

    //Rotation對齊為BOSS的朝向
    //Direction為BOSS朝向
    //BOSS已經面向玩家,所以
    public void Shoot()
    {
        Debug.Log("Already shoot");
        Quaternion rotation = stateMachine.transform.rotation;
        GameObject go = Instantiate(particleprefab, transform.position, rotation);
        ArcMissileSpawner arcMissile =go.GetComponent<ArcMissileSpawner>();
        arcMissile.Direction = stateMachine.transform.forward.normalized;
    }
}
