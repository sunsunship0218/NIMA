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
        Instance = this;
    }
    void Start()
    {
       virtualCamera = GetComponent<CinemachineVirtualCamera>();
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
        CinemachineBasicMultiChannelPerlin multiChannelPerlin =
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
         
        multiChannelPerlin.m_AmplitudeGain = intensity;
        Shaketimer = time;
    }
}
