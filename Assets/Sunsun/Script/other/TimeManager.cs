using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    bool waiting;
    [SerializeField] float slowFactor =0.2f;
    public void DoBulletTime(float duration)
    {
        //�����Ĳ�o
        if (waiting) { return; }
        //����ɶ���t
        Time.timeScale = slowFactor;
        // �վ㪫�z�t�Ϊ��ɶ��Y��
        StartCoroutine(wait(duration));
    }
    IEnumerator wait(float duration)
    {
        waiting = true;
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1f;
    }


}
