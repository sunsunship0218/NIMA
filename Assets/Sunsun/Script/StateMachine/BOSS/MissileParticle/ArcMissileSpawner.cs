using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMissileSpawner : MonoBehaviour
{
    public Vector3 Direction =Vector3.forward;
    Vector3 velocity;
   public float speed;

    private void Start()
    {
        Destroy(gameObject,3);
    }
    private void Update()
    {
        velocity = Direction * speed;
    }
    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos += velocity * Time.fixedDeltaTime;
        transform.position = pos;
    }
   
}
