using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HitCountUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] GameObject hitText;
    int hits = 0;
    void OnAwake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        hitText.SetActive(false);
    }
    void HandleHitcount()
    {
        hitText.SetActive(true);
        hits++;
        textMeshProUGUI.text = hits.ToString();
        //fade out 5 sec
    }
    private void OnEnable()
    {
        WeaponDamage.OnEnemyHit += HandleHitcount;
    }
    private void OnDisable()
    {
        WeaponDamage.OnEnemyHit -= HandleHitcount;

    }
}
