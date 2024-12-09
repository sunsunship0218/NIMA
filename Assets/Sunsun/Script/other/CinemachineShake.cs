using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    //my cinemachine camera
    CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin multiChannelPerlin;
    //timer
    float Shaketimer;
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        multiChannelPerlin =virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

        if (Shaketimer > 0)
        {
            Shaketimer -= Time.deltaTime;
            if (Shaketimer <= 0f)
            {
                //°±¤î§Ý°Ê
                CinemachineBasicMultiChannelPerlin multiChannelPerlin =
              virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                multiChannelPerlin.m_AmplitudeGain = 0f;
                
            }
        }
      
    }
    public void ShakeCamera(float intensity, float time)
    {    
        multiChannelPerlin.m_AmplitudeGain = intensity;
        Shaketimer = time;
    }
    
    IEnumerator WaitTime(float shaketime)
    {
        yield return new WaitForSeconds(shaketime);
    }
    void Intencity()
    {

    }
}
