using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMissile : MonoBehaviour
{
    [SerializeField] private GameObject _missilePrefab;
    [SerializeField] private Transform _positionPointSpawner;
    [SerializeField] private Transform _plaeyr;

    public void Create()
    {
        Instantiate(_missilePrefab, _positionPointSpawner.position, _plaeyr.rotation);
    }
}
