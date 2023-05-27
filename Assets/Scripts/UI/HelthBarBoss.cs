using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthBarBoss : Bar
{
    [SerializeField] private HealthBoss _healthBoss;

    private void OnEnable()
    {
        _healthBoss.OnChangeHealthBoss += OnValueChanged;
        Slider.value = 1;
    }
    private void OnDisable()
    {
        _healthBoss.OnChangeHealthBoss -= OnValueChanged;
    }
}
