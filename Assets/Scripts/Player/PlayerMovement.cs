using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speedMovement;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _groundCheckRadius;

    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;

    public static PlayerMovement Instance { get; private set; }

    public Transform Transform => _groundCheckPoint;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    public void Move(float moveX)
    {
        _rigidbody2D.velocity = new Vector2(moveX * _speedMovement, _rigidbody2D.velocity.y);

        _animator.SetBool("IsRun", moveX != 0);
        _animator.SetBool("IsGrounded", _isGrounded);
    }

    public void CheckSide(float moveX)
    {
        if (moveX == 0) return;

        float scaleX = transform.localScale.x;
        scaleX = moveX > 0 ? Mathf.Abs(scaleX) : Mathf.Abs(scaleX) * -1;

        transform.localScale = new Vector2(scaleX, transform.localScale.y);
    }

    public void Jump()
    {
        if (_isGrounded == false) return;

        _rigidbody2D.velocity = new Vector2(_speedMovement, 0);
        _rigidbody2D.AddForce(Vector2.up * _jumpForce);
        _animator.SetTrigger("Jump");
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheckPoint.position, _groundCheckRadius);

        _isGrounded = false;

        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.isTrigger == false)
            {
                _isGrounded = true;
            }
        }
    }
}
