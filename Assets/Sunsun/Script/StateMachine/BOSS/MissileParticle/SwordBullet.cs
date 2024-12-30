using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�l�u���ͪ���m
public class SwordBullet : MonoBehaviour
{
    public GameObject particleprefab;
    [SerializeField] BossStateMachine stateMachine;
    Vector2 direction;
    //���ͤl�u
    //�b�S�ĩI�s�o�ӵ{��
    //gameobject �O�w�]��m

    //Rotation�����BOSS���¦V
    //Direction��BOSS�¦V
    //BOSS�w�g���V���a,�ҥH
    public void Shoot()
    {
        Debug.Log("Already shoot");
        Quaternion rotation = stateMachine.transform.rotation;
        GameObject go = Instantiate(particleprefab, transform.position, rotation);
        ArcMissileSpawner arcMissile =go.GetComponent<ArcMissileSpawner>();
        arcMissile.Direction = stateMachine.transform.forward.normalized;
    }
}
