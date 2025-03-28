using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HitCountUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    int hits = 0;
    void OnAwake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.gameObject.SetActive(false);
    }
    void HandleHitcount()
    {
        textMeshProUGUI.gameObject.SetActive(true);
        hits++;
        textMeshProUGUI.text = hits.ToString();
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
