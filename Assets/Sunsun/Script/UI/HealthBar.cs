using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NIMA.UI
{
    public class HealthBar : MonoBehaviour
    {

        public Slider PlayerHealthSlider;
        public Slider Npc1HealthSlider;

        //血條初始最大值
        public void SetSliderMax(float amount)
        {
            PlayerHealthSlider.maxValue = amount;
            SetSlider(amount);
        }
        //改變血條值
        public void SetSlider(float amount)
        {
            PlayerHealthSlider.value = amount;
        }
     
     
    }
}
