using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Expose

    [SerializeField] float _walkSpeed = 5f;
    [SerializeField] float _runSpeed = 10f;
    [SerializeField] int _health = 20;
    [SerializeField] AnimationCurve _jumpCurve;
    [SerializeField] float _jumpHeight = 2f;
    [SerializeField] float _jumpDuration = 1f;

    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _graphicsTransform = transform.Find("Graphics");

    }

    void Start()
    {

    }

    void Update()
    {

        _direction = new Vector2(Input.GetAxisRaw("Horizontal") * _walkSpeed, Input.GetAxisRaw("Vertical") * _walkSpeed);

        if (Input.GetAxisRaw("Fire3") == 1)
        {
            _direction = new Vector2(Input.GetAxisRaw("Horizontal") * _runSpeed, Input.GetAxisRaw("Vertical") * _runSpeed);
        }
        TurnCharacter();
        Jump();
        ActivateAnimation();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _direction;
    }


    #endregion

    #region Methods

    private void ActivateAnimation()
    {
        float maxValue = Mathf.Max(Mathf.Abs(_direction.x), Mathf.Abs(_direction.y));
        _animator.SetFloat("moveSpeedX", maxValue);
        if (_isJumping == true)
        {
            _animator.SetBool("isJumping", true);
        }
        else
        {
            _animator.SetBool("isJumping", false);
        }
        if (_isLanding)
        {
            _animator.SetBool("isLanding", true);
        }
        else
        {
            _animator.SetBool("isLanding", false);
        }
        if (_health <= 0)
        {
            _animator.SetInteger("healthPoints", 0);
        }
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

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _isLanding = false;
            _isJumping = true;

        }
        if (_isJumping == true)
        {
            if (_jumpTimer < _jumpDuration)
            {
                _jumpTimer += Time.deltaTime;

                // Progression / maximum // Donne le pourcentage restant
                float curveY = _jumpCurve.Evaluate(_jumpTimer / _jumpDuration);

                _graphicsTransform.localPosition = new Vector2(_graphicsTransform.localPosition.x, curveY * _jumpHeight/*, _graphicsTransform.localPosition.z*/);
                Debug.Log("timer" + _jumpTimer);
            }

            else if (_jumpTimer >= _jumpDuration)
            {
                _jumpTimer = 0f;
                _isJumping = false;
                _isLanding = true;
            }
        }
    }

    #endregion

    #region Private & Protected
    Rigidbody2D _rigidbody;
    Animator _animator;
    Vector2 _direction;
    Transform _graphicsTransform;
    float _jumpTimer;
    bool _isJumping;
    private bool _isLanding;


    #endregion
}
