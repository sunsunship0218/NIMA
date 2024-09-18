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
        //控制時間減速
        Time.timeScale = slowFactor;
        // 調整物理系統的時間縮放
        StartCoroutine(wait(duration));
    }
    IEnumerator wait(float duration)
    {
        waiting = true;
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1f;
    }


}
