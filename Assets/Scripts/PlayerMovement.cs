using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Expose

    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _runSpeed = 10f;
    [SerializeField] int _health = 20;

    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //float _vertical = Input.GetAxisRaw("Vertical") * _moveSpeed;
        //float _horizontal = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        //float maxValue = Mathf.Max(Mathf.Abs(_vertical), Mathf.Abs(_horizontal));
    }

    void Update()
    {
        
        _direction = new Vector2(Input.GetAxisRaw("Horizontal") * _moveSpeed, Input.GetAxisRaw("Vertical") * _moveSpeed);
        //_direction = new Vector2(maxValue, maxValue);


        if (Input.GetAxisRaw("Fire3") == 1)
        {
            _direction = new Vector2(Input.GetAxisRaw("Horizontal") * _runSpeed, Input.GetAxisRaw("Vertical") * _runSpeed);
        }
        ActivateAnimation();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _direction;
        TurnCharacter();
    }


    #endregion

    #region Methods

    private void ActivateAnimation()
    {
        float maxValue = Mathf.Max(Mathf.Abs(_direction.x), Mathf.Abs(_direction.y));
        _animator.SetFloat("moveSpeedX", maxValue);
        //if (Input.GetAxis("Jump") == 1)
        //{
        //    _animator.SetBool("isJumping", true);
        //}
        //else
        //{
        //    _animator.SetBool("isJumping", false);
        //}
    }

    private void TurnCharacter()
    {
        // Tourner le personnage dans la bonne direction
        if (_direction.x < 0f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (_direction.x > 0f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    #endregion

    #region Private & Protected
    Rigidbody2D _rigidbody;
    Animator _animator;
    Vector2 _direction;

    #endregion
}
