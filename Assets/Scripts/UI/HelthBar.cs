using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthBar : Bar
{
    [SerializeField] private HealthPlayer _healthPlayer;

    private void OnEnable()
    {
        _healthPlayer.OnChangeHealthPlayer += OnValueChanged;
        Slider.value = 1;
    }
    private void OnDisable()
    {
        _healthPlayer.OnChangeHealthPlayer -= OnValueChanged;
    }
}
