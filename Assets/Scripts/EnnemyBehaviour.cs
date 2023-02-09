using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnnemyState
{
    IDLE,
    WALK,
    ATTACK,
    AIRATTACK,
    JUMP,
    DEAD
}
public class EnnemyBehaviour : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float _limitNearTarget = 0.5f;

    [SerializeField]
    private float _waitingTimeBeforeAttack = 1f;

    [SerializeField]
    private float _attackDuration = 0.2f;

    #endregion



    // Start is called before the first frame update
    private void Start()
    {
        TransitionToState(EnnemyState.IDLE);
        _moveTarget = GameObject.Find("Player").transform;

    }

    // Update is called once per frame
    private void Update()
    {
        OnStateUpdate();
    }

    private void FixedUpdate()
    {
        /*Vector2 directionToPlayer = _moveTarget.position - transform.position;
        Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, rotationToPlayer, _speed * Time.deltaTime);

        _rigidbody.velocity = transform.position * _speed;
        _rigidbody.MoveRotation(rotation);
        */

        // Tourner le personnage dans la bonne direction : 2 Methodes(avec le scale et avec le transform.right)
        /*if (_directionToPlayer.x < 0f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (_directionToPlayer.x > 0f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }*/

    }

    #region Main Methods

    void OnStateEnter()
    {
        switch (_currentState)
        {
            case EnnemyState.IDLE:
                _attackTimer = 0f;
                break;
            case EnnemyState.WALK:
                _animator.SetBool("isWalking", true);
                break;
            case EnnemyState.ATTACK:
                _attackTimer = 0f;
                _animator.SetBool("isAttacking", true);
                break;
            case EnnemyState.AIRATTACK:
                _animator.SetBool("isAirAttacking", true);
                break;
            case EnnemyState.JUMP:
                _animator.SetBool("isJumping", true);
                break;
            default:
                break;
            case EnnemyState.DEAD:
                break;
        }
    }

    void OnStateUpdate()
    {
        switch (_currentState)
        {
            case EnnemyState.IDLE:


                //J'ai détecté le player ET je suis loin de lui
                if(_playerDetected && !IsTargetNearLimit())
                {
                    Turn();
            
                    TransitionToState(EnnemyState.WALK);
                }

                if(IsTargetNearLimit())
                {
                    //J'execute ce code quand je suis en IDLE prêt du joueur
                    //Compteur de seconde
                    //TransitionToState pour changer quand c'est bon
                    _attackTimer += Time.deltaTime;
                    if(_attackTimer >= _waitingTimeBeforeAttack)
                   {
                        TransitionToState(EnnemyState.ATTACK);
                   }
                }
                break;
            case EnnemyState.WALK:

                transform.position = Vector2.MoveTowards(transform.position, _moveTarget.position, Time.deltaTime);
                
                if (IsTargetNearLimit())
                {
                    TransitionToState(EnnemyState.IDLE); 
                }
                if (!_playerDetected)
                {
                    TransitionToState(EnnemyState.IDLE);
                }
                break;
            case EnnemyState.ATTACK:
                _attackTimer += Time.deltaTime;
                if(_attackTimer >= _attackDuration)
                {
                    //J'ai fini d'attaquer
                    TransitionToState(EnnemyState.IDLE);
                }

                break;
            case EnnemyState.AIRATTACK:
                break;
            case EnnemyState.JUMP:
                break;
            default:
                break;
            case EnnemyState.DEAD:
                break;
        }

    }

    void OnStateExit()
    {
        switch (_currentState)
        {
            case EnnemyState.IDLE:
                break;
            case EnnemyState.WALK:
                _animator.SetBool("isWalking", false);
                break;
            case EnnemyState.ATTACK:
                _animator.SetBool("isAttacking", false);
                break;
            case EnnemyState.AIRATTACK:
                _animator.SetBool("isAirAttacking", false);
                break;
            case EnnemyState.JUMP:
                _animator.SetBool("isJumping", false);
                break;
            default:
                break;
            case EnnemyState.DEAD:
                break;
        }
    }

    void TransitionToState(EnnemyState nextState) 
    {
        OnStateExit();
        _currentState = nextState;
        OnStateEnter();
    }

    void PlayerDetected()
    {
        Debug.Log("J'ai détecté le joueur");
        _playerDetected = true;

    }

    void PlayerEscaped()
    {
        _playerDetected = false;
    }

    bool IsTargetNearLimit()
    {
        return Vector2.Distance(transform.position, _moveTarget.position) < _limitNearTarget;
    }

    /* void Flip()
     {
         Vector2 _scale = transform.localScale;
         _scale.x *= -1;
         transform.localScale = _scale;
         _facingRight = !_facingRight;

         if (Vector2.Distance(_moveTarget.position, transform.position) < 10)
         {
             transform.position = Vector2.MoveTowards(transform.position, _moveTarget.position, _speed * Time.deltaTime);
             if (_moveTarget.position.x > transform.position.x && !_facingRight) //if the target is to the right of enemy and the enemy is not facing right
                 Flip();
             if (_moveTarget.position.x < transform.position.x && _facingRight)
                 Flip();
         }

     }*/

    void Turn()
    {
        Vector3 scale = transform.localScale;

        if (_moveTarget.transform.position.x < transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (_flip ? -1 : 1);
            transform.Translate(_speed * Time.deltaTime, 0, 0);

        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (_flip ? -1 : 1);
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }
        transform.localScale = scale;
    }
    #endregion

    #region Privates

    private EnnemyState _currentState;
    private bool _playerDetected = false;
    private Transform _moveTarget;
    private float _attackTimer;
    private Vector3 _scale;
    private bool _facingRight;
    Vector2 directionToPlayer;
    private Rigidbody2D _rigidbody;
    private bool _flip;
    private float _speed;
    
}
    
    #endregion
