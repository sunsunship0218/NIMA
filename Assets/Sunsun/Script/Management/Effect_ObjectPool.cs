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
        //��l�ƪ����,��ͦ����S�ĩ�쪫���
        for(int count =0; count < initialize; count++)
        {
            ParticleSystem particle =Instantiate(EffectPrefab) as ParticleSystem;
            pool.Enqueue(particle);
            particle.gameObject.SetActive(false);
         
        }
    }
    public void ReuseParticlePlay(Vector3 hitposition,Quaternion quaternion)
    {
        //�����󪺸ܨ��X
        if(pool.Count > 0)
        {
            ParticleSystem reusePar = pool.Dequeue() ;
            //�w����X�᪺��m
            reusePar.gameObject.SetActive(true);
            reusePar.transform.position = hitposition;
            reusePar.transform.rotation = quaternion;
            reusePar.Play( );
        }
        //�S�����ܭ��s����
        else
        {
           ParticleSystem go = Instantiate(EffectPrefab) as ParticleSystem;
            go.transform.position = hitposition;
            go.transform.rotation = quaternion;
        }
       
    }
    //�^������,��J����
    void Recovery(ParticleSystem recovery)
    {
        pool.Enqueue(recovery);
        recovery.Stop();
    }
    // Update is called once per frame
}
