using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBullet : MonoBehaviour
{
    public ArcMissileSpawner spawner;
    Vector2 direction;
    //產生子彈
    //在特效呼叫這個程式
    //gameobject 預設位置
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
