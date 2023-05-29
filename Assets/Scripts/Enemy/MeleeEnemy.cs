using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class MeleeEnemy : MonoBehaviour
{
    private const string IsRun = "IsRun";
    private const string IsSwordAttack = "IsSwordAttack";
    private const string OnAttack = "OnAttack";

    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _pointsMove;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _attackDelay;
    [SerializeField] private Health _health;
    [SerializeField] private int _damage;

    private State _state;   
    private Vector3 _currentTarget;
    private int _currentIndex;    
    private IEnumerator _attackCoroutine;

    private enum State
    {
        MoveTowards,
        ChaseTarget,
        AttackPlayer,
    }

    private void Awake()
    {
        _state = State.MoveTowards;
    }
    private void Start()
    {       
        _currentTarget = _pointsMove[0].position;

        _health.OnZeroHealth += () =>
        {
            ExpirienceManager.Instance.Value += 1;
        };
    }

    private void Update()
    {
        WorkStateMachine();
    }

    private void WorkStateMachine()
    {
        switch (_state)
        {
            case State.MoveTowards:
                MoveTarget(_currentTarget);
                CheckTarget();
                FindPlayer();

                _animator.SetBool(IsRun, true);
                break;
            case State.ChaseTarget:

                if (PlayerMovement.Instance != null)
                {
                    MoveTarget(PlayerMovement.Instance.Transform.position);
                    ReturningPatrol();
                }
                else
                {
                    _state = State.MoveTowards;
                }

                break;
            case State.AttackPlayer:

                if (PlayerMovement.Instance != null)
                {
                    MoveTarget(PlayerMovement.Instance.Transform.position);
                }
                else
                {
                    _animator.SetBool(IsSwordAttack, false);
                    _state = State.MoveTowards;
                }

                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<HealthPlayer>(out var player) == false)
            return;

        if (_state == State.AttackPlayer)
            return;

        _state = State.AttackPlayer;

        _attackCoroutine = AttackDelay(player);

        StartCoroutine(_attackCoroutine);

        _animator.SetBool(IsSwordAttack, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<HealthPlayer>(out var player) == false) return;

        _state = State.ChaseTarget;

        StopCoroutine(_attackCoroutine);

        _animator.SetBool(IsSwordAttack, false);
    }

    private void MoveTarget(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
        Vector2 moveX = target - transform.position;
        CheckSide(moveX.x);
    }

    private void CheckSide(float moveX)
    {        
        if (moveX == 0) return;

        float scaleX = transform.localScale.x;
        scaleX = moveX > 0 ? Mathf.Abs(scaleX) : Mathf.Abs(scaleX) * -1;

        transform.localScale = new Vector2(scaleX, transform.localScale.y);
    }

    private void CheckTarget()
    {
        if(transform.position == _currentTarget)
        {
            ChangeTarget();
        }
    }

    private void ChangeTarget()
    {
        _currentIndex++;

        if( _currentIndex >= _pointsMove.Length)
        {
            _currentIndex = 0;
        }

        _currentTarget = _pointsMove[_currentIndex].position;
    }

    private void FindPlayer()
    {
        float distance = 7;

        if (PlayerMovement.Instance == null) return;

        if (Vector3.Distance(transform.position, PlayerMovement.Instance.Transform.position) < distance)
        {
            _state = State.ChaseTarget;
        }
    }

    private void ReturningPatrol()
    {
        float distance = 7;

        if (Vector3.Distance(transform.position, PlayerMovement.Instance.Transform.position) > distance)
        {
            _state = State.MoveTowards;
        }
    }

    private IEnumerator AttackDelay(HealthPlayer healthPlayer)
    {
        while (_state == State.AttackPlayer)
        {
            Attack(healthPlayer);
            yield return new WaitForSeconds(_attackDelay);
        }
    }

    private void Attack(HealthPlayer healthPlayer)
    {
        healthPlayer.Value -= _damage;

        if (healthPlayer.Value <= 0)
        {
            _state = State.ChaseTarget;

            StopCoroutine(_attackCoroutine);

            _animator.SetBool(IsSwordAttack, false);
        }

        _animator.SetTrigger(OnAttack);
    }
}