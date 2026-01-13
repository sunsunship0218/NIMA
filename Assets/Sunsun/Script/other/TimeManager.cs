using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    bool waiting;
    float originalFixedDeltaTime;
    [SerializeField] float slowFactor =0.2f;
    bool busy;
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
        //   Time.fixedDeltaTime = Time.timeScale * slowFactor;
        Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
        //      Debug.Log("DO Bullet time: "+ slowFactor);
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

    public void DoHitStop  (
       float freeze = 0.02f,   // 完全停止
       float slow = 0.05f,   // 慢動作
       float slowFac = 0.3f     // 慢動作倍率
    )
    {
        if (busy) return;
        StartCoroutine(HitStopRoutine(freeze, slow, slowFac));
    }

    IEnumerator HitStopRoutine(float freeze, float slow, float fac)
    {
        busy = true;

        /*  Freeze */
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        yield return new WaitForSecondsRealtime(freeze);

        /*  Slow */
        Time.timeScale = fac;
        Time.fixedDeltaTime = originalFixedDeltaTime * fac;
        yield return new WaitForSecondsRealtime(slow);

        /*  Recover */
        Time.timeScale = 1f;
        Time.fixedDeltaTime = originalFixedDeltaTime;
        busy = false;
    }

}
