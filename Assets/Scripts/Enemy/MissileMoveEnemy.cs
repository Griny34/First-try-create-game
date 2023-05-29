using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MissileMoveEnemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _velosity;
    private float _lifeTime = 3;

    public Vector3 Sight { get; set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Launch();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<HealthPlayer>(out var health) == true)
        {
            health.Value -= _damage;

            Destroy(gameObject);
        }
    }

    public void Launch()
    {
        Vector3 sight = Sight;

        _velosity = sight - transform.position;

        _rigidbody2D.velocity = _velosity * _speed;

        Vector3 rotation = transform.position - sight;

        float rot = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        Destroy(gameObject, _lifeTime);
    }
}
