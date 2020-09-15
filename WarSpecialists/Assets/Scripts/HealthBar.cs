using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider mSlider;
    public void setMaxHealth(float health)
    {
        mSlider.maxValue = health;
        mSlider.value = health;
    }

    public void setHealth(float health)
    {
        mSlider.value = health;
    }
}
