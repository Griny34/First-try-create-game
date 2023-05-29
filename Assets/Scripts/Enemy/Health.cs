using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _startValue;

    private int _value;

    public int Value
    {
        get => _value;
        set
        {
            _value = value;

            if (_value <= 0)
            {
                OnZeroHealth?.Invoke();

                _value = 0;

                Destroy(gameObject);
            }

            OnChangeHealth?.Invoke(_value, _startValue);
        }
    }

    public event Action<int,int> OnChangeHealth;
    public event Action OnZeroHealth;

    private void Awake()
    {
        _value = _startValue;
    }
}
