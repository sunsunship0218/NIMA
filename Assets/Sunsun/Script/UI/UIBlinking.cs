using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBlinking : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    void Start()
    {
        text =GetComponent<TextMeshProUGUI>();
        StartBlinking();
    }

    IEnumerator Blink()
    {

        while (true)
        {
            text.alpha = text.alpha == 0f ? 1f : 0f;
            yield return new WaitForSeconds(0.5f);
        }
    }
    void StartBlinking()
    {
        StartCoroutine("Blink");
    }
    void StopBlinking()
    {
        StopCoroutine("Blink");
    }
}
