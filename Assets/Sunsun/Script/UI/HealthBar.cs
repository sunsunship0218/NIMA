using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public Slider healthBar;
    public float maxHealth;
    private void Start()
    {
        maxHealth = gameManager.player.playerHP;
        }

}

