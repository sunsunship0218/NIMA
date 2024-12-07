using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PostureBar : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    public RectTransform fillTransform;
    public Slider Posturebar;
    public float maxPosture;
    public float Posture;
    // Start is called before the first frame update
    void Start()
    {
        maxPosture = playerHealth.healthSystem.GetPostureAmountMax();
       Posturebar.maxValue = maxPosture;
       Posturebar.value = maxPosture;
        playerHealth.healthSystem.OnPostureChange+= HealthSystem_OnPostureChange;
    }
    void UpdateFillWidth(float normalizedValue)
    {
        if (fillTransform != null)
        {         
            Vector3 scale = fillTransform.localScale;
            scale.x = normalizedValue; 
            fillTransform.localScale = scale;
        }
    }
    private void HealthSystem_OnPostureChange(object sender, System.EventArgs e)
    {
        //更新現在血量
       Posture= playerHealth.healthSystem.GetPostureAmount();
        //更新UI
        UpdateFillWidth(Posture / maxPosture);
     
    }
}
