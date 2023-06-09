using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerMissileEnemy : MonoBehaviour
{
    [SerializeField] private Transform _sightPoint;
    [SerializeField] private MissileMoveEnemy _prefabMissile;
    [SerializeField] private Transform _positionPointSpawner;

    public void Create()
    {
        var missle = Instantiate(_prefabMissile, _positionPointSpawner.position, Quaternion.identity);
        missle.Sight = _sightPoint.position;
        //missle.Launch();
    }
}
