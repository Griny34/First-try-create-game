using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private int _startValue;
    [SerializeField] private GameObject _deathPanel;

    private int _value;

    public int Value
    {
        get => _value;
        set
        {
            _value = value;

            if (_value <= 0)
            {
                OnZeroHealthPlayer?.Invoke();

                _value = 0;
                Destroy(gameObject);
            }

            OnChangeHealthPlayer?.Invoke(_value, _startValue);
        }
    }

    public delegate void ChangeHealth(int value, int maxValue);
    public event ChangeHealth OnChangeHealthPlayer;

    public delegate void ZeroHealth();
    public event ZeroHealth OnZeroHealthPlayer;

    private void Awake()
    {
        _value = _startValue;
    }

    private void Start()
    {
        OnZeroHealthPlayer += () =>
        {
            _deathPanel.SetActive(true);
        };
    }
}
