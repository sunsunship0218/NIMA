using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathPanel : MonoBehaviour
{
    //UI
    [SerializeField]
    GameObject  Panel;

    [SerializeField] PlayerHealth playerHealth;
    private void Awake()
    {
        Panel.SetActive(false);
    }
    void HnadleDeathPanel()
    {
        Panel.SetActive(true);
    }
    private void OnEnable()
    {
        playerHealth.healthSystem.OnDie += HnadleDeathPanel;
    }
    private void OnDisable()
    {
        playerHealth.healthSystem.OnDie -= HnadleDeathPanel;
    }
  
}
