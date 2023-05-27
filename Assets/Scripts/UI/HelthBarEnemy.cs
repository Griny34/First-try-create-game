using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthBarEnemy : Bar
{
    [SerializeField] private Health _healthEnemy;

    private void OnEnable()
    {
        _healthEnemy.OnChangeHealth += OnValueChanged;
        Slider.value = 1;
    }
    private void OnDisable()
    {
        _healthEnemy.OnChangeHealth -= OnValueChanged;
    }
}
