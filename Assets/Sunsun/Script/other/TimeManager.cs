using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    bool waiting;
    float originalFixedDeltaTime;
    [SerializeField] float slowFactor =0.2f;
    private void Awake()
    {
        originalFixedDeltaTime = Time.fixedDeltaTime;
    }
    public void DoBulletTime(float duration)
    {
        //防止重複觸發
        if (waiting) { return; }
        
        //控制時間減速
        Time.timeScale = slowFactor;
        // 調整物理系統的時間縮放
        Time.fixedDeltaTime = Time.timeScale * slowFactor;
       
        StartCoroutine(wait(duration));
    }
    IEnumerator wait(float duration)
    {
        float startRealTime = Time.realtimeSinceStartup;
        waiting = true;
        yield return new WaitForSecondsRealtime((duration));
        float endRealTime = Time.realtimeSinceStartup;
       
        Time.timeScale = 1f;
        //reset 回 正常時間流速
        Time.fixedDeltaTime = 0.02f; // 重置fixedDeltaTime
        
        waiting = false; // 重置waiting标志
    }


}
