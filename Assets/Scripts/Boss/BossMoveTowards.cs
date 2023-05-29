using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveTowards : MonoBehaviour
{
    [SerializeField] private Transform[] _pointMoveTowards;
    [SerializeField] private float _speed;

    private Vector3 _currentTarget;
    private int _currentIndex;

    private void Start()
    {
        _currentTarget = _pointMoveTowards[0].position;
    }
    private void Update()
    {
        ChackTarget();
        MoveTarget();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<MissileBoss>(out var missile) == true)
        {
            Destroy(collision.gameObject);
        }
    }

    private void MoveTarget()
    {
       transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
    }

    private void ChackTarget()
    {
        if(transform.position == _currentTarget)
        {
            ChangTarget();
        }
    }

    private void ChangTarget()
    {
        _currentIndex++;

        if(_currentIndex >= _pointMoveTowards.Length)
        {
            _currentIndex = 0;
        }

        _currentTarget = _pointMoveTowards[_currentIndex].position;
    }
}
