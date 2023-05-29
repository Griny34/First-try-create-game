using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(Animator))]
public class RangedEnemy : MonoBehaviour
{
    private const string IsRun = "IsRun";
    private const string IsShoot = "IsShoot";
    private const string Shoot = "Shoot";

    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _pointsMove;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _timeAttack;
    [SerializeField] private SpawnerMissileEnemy _missileEnemy;
    [SerializeField] private Health _health;

    private State _state;
    private Vector3 _currentTarget;
    private int _currentIndex;
    private bool _isRun = true;
    
    public enum State
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
            default:
            case State.MoveTowards:
                MoveTarget(_currentTarget);
                CheckTarget();
                _animator.SetBool(IsRun, _isRun);
                FindPlayer();
                break;
            case State.ChaseTarget:

                if (PlayerMovement.Instance != null)
                {
                    MoveTarget(PlayerMovement.Instance.Transform.position);
                    ReturningPatrol();
                    Attack();
                }
                else
                {
                    _state = State.MoveTowards;
                }

                break;
            case State.AttackPlayer:

                if (PlayerMovement.Instance != null)
                {
                    ShootMissile();
                    LossPurpose();
                }
                else
                {
                    _state = State.MoveTowards;
                }

                break;
        }
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
        if (transform.position == _currentTarget)
        {
            ChangeTarget();
        }
    }

    private void ChangeTarget()
    {
        _currentIndex++;

        if (_currentIndex >= _pointsMove.Length)
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

    private void Attack()
    {
        float distance = 5;

        if(Vector3.Distance(transform.position, PlayerMovement.Instance.Transform.position) < distance)
        {
            _animator.SetBool(IsRun, false);
            _state = State.AttackPlayer;
        } 
    }

    private void LossPurpose()
    {
        float distance = 6;

        if (Vector3.Distance(transform.position, PlayerMovement.Instance.Transform.position) > distance)
        {
            _animator.SetBool(IsShoot, false);
            _animator.SetBool(IsRun, true);
            _state = State.ChaseTarget;
        }
    }

    private void ShootMissile()
    {
        if (Time.time > _timeAttack)
        {
            _animator.SetTrigger(Shoot);
            _missileEnemy.Create();

            float timedelay = 1;

            _timeAttack = Time.time + timedelay;
        }
    }
}
