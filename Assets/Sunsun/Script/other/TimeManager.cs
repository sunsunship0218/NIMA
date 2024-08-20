using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // §ðÀ» ®æÄÒ
    public float slowDownFactor = 0.05f;
    public float slowDownLength = 1f;

    void Update()
    {
        Time.timeScale += (1f / slowDownLength)*Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
    public void DoBulletTime()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
       
    }
}
