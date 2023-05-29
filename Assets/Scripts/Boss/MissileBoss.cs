using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MissileBoss : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _velosity;
    private float _lifeTime = 3;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Initialize();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<HealthPlayer>(out var health) == true)
        {
            health.Value -= _damage;

            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        Vector3 target = PlayerMovement.Instance.transform.position;

        _velosity = target - transform.position;

        _rigidbody2D.velocity = _velosity * _speed;

        Vector3 rotation = transform.position - target;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rot + 180);

        Destroy(gameObject, _lifeTime);
    }
}
