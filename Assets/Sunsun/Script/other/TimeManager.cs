using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    bool waiting;
    [SerializeField] float slowFactor =0.2f;
    public void DoBulletTime(float duration)
    {
        //防止重複觸發
        if (waiting) { return; }
        Debug.Log($"Starting Bullet Time. Duration: {duration} seconds. Slow Factor: {slowFactor}");
        Debug.Log($"Before slowdown: Time.timeScale = {Time.timeScale}, Time.fixedDeltaTime = {Time.fixedDeltaTime}");
        //控制時間減速
        Time.timeScale = slowFactor;
        // 調整物理系統的時間縮放
        Time.fixedDeltaTime = Time.timeScale * slowFactor;
        Debug.Log($"After slowdown: Time.timeScale = {Time.timeScale}, Time.fixedDeltaTime = {Time.fixedDeltaTime}");
        StartCoroutine(wait(duration));
    }
    IEnumerator wait(float duration)
    {
        float startRealTime = Time.realtimeSinceStartup;
        waiting = true;
        yield return new WaitForSecondsRealtime((duration));
        float endRealTime = Time.realtimeSinceStartup;
        Debug.Log($"End wait. Real time elapsed: {endRealTime - startRealTime} seconds.");
        Time.timeScale = 1f;
        //reset 回 正常時間流速
        Time.fixedDeltaTime = 0.02f; // 重置fixedDeltaTime
        Debug.Log($"After recovery: Time.timeScale = {Time.timeScale}, Time.fixedDeltaTime = {Time.fixedDeltaTime}");
        waiting = false; // 重置waiting标志
    }


}
