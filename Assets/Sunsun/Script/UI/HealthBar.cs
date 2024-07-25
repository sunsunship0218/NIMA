using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NIMA.UI
{
    public class HealthBar : MonoBehaviour
    {

        public Slider healthSlider;
        
        public void SetSlider(float amount)
        {
            healthSlider.value = amount;
        }
        public void SetSliderMax(float amount)
        {
            healthSlider.maxValue = amount;
            SetSlider(amount);
        }
     
    }
}
