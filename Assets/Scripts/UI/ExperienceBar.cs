using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBar : Bar
{
    private void OnEnable()
    {
        ExpirienceManager.Instance.OnChangeExperience += OnValueChanged;
        Slider.value = 0;
    }

    private void OnDisable()
    {
        ExpirienceManager.Instance.OnChangeExperience -= OnValueChanged;
    }
}
