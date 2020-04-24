using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatBars : MonoBehaviour
{
    public Slider slider;
    public void SetMaxValue(float value, float current )
    {
        slider.maxValue = value;
        slider.value = current;
    }
   
    public void SetValue(float value)
    {
        slider.value= value;
    }
}


