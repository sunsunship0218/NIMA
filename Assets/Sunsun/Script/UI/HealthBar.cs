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

        //�����l�̤j��
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

        //���ܦ����
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
