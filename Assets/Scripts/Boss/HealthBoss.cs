using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoss : MonoBehaviour
{
    [SerializeField] private int _startValue;
    [SerializeField] private GameObject _panel;

    private int _value;

    public int Value
    {
        get => _value;
        set
        {
            _value = value;

            if (_value <= 0)
            {
                OnZeroHealthBoss?.Invoke();

                _value = 0;
                Destroy(gameObject);
                _panel.SetActive(true);               
            }

            OnChangeHealthBoss?.Invoke(_value, _startValue);
        }
    }

    public delegate void ChangeHealth(int value, int maxValue);
    public event ChangeHealth OnChangeHealthBoss;

    public delegate void ZeroHealth();
    public event ZeroHealth OnZeroHealthBoss;

    private void Awake()
    {
        _value = _startValue;
    }
}
