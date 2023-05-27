using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider Slider;

    private int _maxValue = 3;

    public void OnValueChanged(int value, int maxValue)
    {
        Slider.value = (float)value / maxValue;
    }

    public void OnValueChanged(int value)
    {
        Slider.value = (float)value / _maxValue;
    }
}
