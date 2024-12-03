using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_ObjectPool : MonoBehaviour
{
   public ParticleSystem EffectPrefab;
    [SerializeField] int initialize = 10;
    Queue<ParticleSystem> pool =new Queue<ParticleSystem>();
    void Awake()
    {
        //初始化物件池,把生成的特效放到物件池
        for(int count =0; count < initialize; count++)
        {
            ParticleSystem particle =Instantiate(EffectPrefab) as ParticleSystem;
            pool.Enqueue(particle);
            particle.gameObject.SetActive(false);
         
        }
    }
    public void ReuseParticlePlay(Vector3 hitposition,Quaternion quaternion)
    {
        //有物件的話取出
        if(pool.Count > 0)
        {
            ParticleSystem reusePar = pool.Dequeue() ;
            //定位取出後的位置
            reusePar.gameObject.SetActive(true);
            reusePar.transform.position = hitposition;
            reusePar.transform.rotation = quaternion;
            reusePar.Play( );
        }
        //沒有的話重新產生
        else
        {
           ParticleSystem go = Instantiate(EffectPrefab) as ParticleSystem;
            go.transform.position = hitposition;
            go.transform.rotation = quaternion;
        }
       
    }
    //回收物件,放入池中
    void Recovery(ParticleSystem recovery)
    {
        pool.Enqueue(recovery);
        recovery.Stop();
    }
    // Update is called once per frame
}
