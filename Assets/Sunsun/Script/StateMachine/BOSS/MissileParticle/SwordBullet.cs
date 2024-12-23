using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBullet : MonoBehaviour
{
    public ArcMissileSpawner spawner;
    Vector2 direction;
    //���ͤl�u
    //�b�S�ĩI�s�o�ӵ{��
    //gameobject �w�]��m
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        direction = (transform.localRotation * Vector2.right).normalized;
    }
    public void Shoot()
    {
        GameObject go = Instantiate(spawner.gameObject, transform.position, Quaternion.identity);
        ArcMissileSpawner arcMissile =go.GetComponent<ArcMissileSpawner>();
        arcMissile.Direction = direction;
    }
}
