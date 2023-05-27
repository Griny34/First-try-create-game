using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerEvents : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private Transform _positionPointsEvent;

    private void Create()
    {
        GameObject _prefab = _prefabs[Random.Range(0, _prefabs.Length)];

        Instantiate(_prefab, _positionPointsEvent.position, PlayerMovement.Instance.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(PlayerMovement.Instance == null)
            return;

        if (collision.transform.TryGetComponent<HealthBoss>(out var boss) == true)
        {
            Create();
        }
    }
}
