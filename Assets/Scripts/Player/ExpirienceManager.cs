using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpirienceManager : MonoBehaviour
{
    [SerializeField] private int _targetLevelExp;

    private int _value;
    private int _level;

    public static ExpirienceManager Instance { get; private set; }

    public int Value
    {
        get => _value;
        set
        {
            _value = value;

            if (_value >= _targetLevelExp)
            {
                OnLevelUp?.Invoke(++_level);

                _value = 0;
            }

            OnChangeExperience?.Invoke(_value);
        }
    }

    public event Action<int> OnChangeExperience;
    public event Action<int> OnLevelUp;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
