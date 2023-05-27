using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightMissile : MonoBehaviour
{
    public static SightMissile Instance { get; private set; }

    [SerializeField] private GameObject _finishPointMissile;
    public GameObject FinishPointMissile => _finishPointMissile;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
