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
        public void SetPlayerSliderMax(float amount)
        {
            PlayerHealthSlider.maxValue = amount;
            SetPlayerSlider(amount);
        }

        public void SetNpc1SliderMax(float amount)
        {
            PlayerHealthSlider.maxValue = amount;
            SetPlayerSlider(amount);
        }

        //改變血條值
        public void SetPlayerSlider(float amount)
        {
            Npc1HealthSlider.value = amount;
        }
        public void SetNpc1Slider(float amount)
        {
            Npc1HealthSlider.value = amount;
        }


    }
}
