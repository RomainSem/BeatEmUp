using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    #region Expose
    [Header("Movement")]
    [SerializeField] float _walkSpeed = 5f;
    [SerializeField] float _runSpeed = 10f;
    [Header("Jump")]
    [SerializeField] AnimationCurve _jumpCurve;
    [SerializeField] float _jumpHeight = 2f;
    [SerializeField] float _jumpDuration = 1f;
    [Header("Health")]
    [SerializeField] float _maxHealth = 20f;
    [SerializeField] float _health = 20f;
    [SerializeField] Image _healthBar;
    [Header("Cans")]
    [SerializeField] GameObject _greenCan;
    [SerializeField] GameObject _redCan;
    [Header("Fight")]
    [SerializeField] BoxCollider2D _fistCollider;


    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _graphicsTransform = transform.Find("Graphics");
        _enemy = GameObject.FindGameObjectWithTag("Ennemy1");


    }

    void Start()
    {

    }

    void Update()
    {
        Move();
        TurnCharacter();
        Jump();
        Hit();
        Health();
        ActivateAnimation();

    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _direction;
    }


    #endregion

    #region Methods

    private void Move()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal") * _walkSpeed, Input.GetAxisRaw("Vertical") * _walkSpeed);

        if (Input.GetAxisRaw("Fire3") == 1)
        {
            _direction = new Vector2(Input.GetAxisRaw("Horizontal") * _runSpeed, Input.GetAxisRaw("Vertical") * _runSpeed);
        }
    }

    private void Hit()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _isFighting = true;
            StartCoroutine(Fight());

        }
        else
        {
            _isFighting = false;
        }
    }

    IEnumerator Fight()
    {
        yield return new WaitForSeconds(0.1f);
        _fistCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _fistCollider.enabled = false;

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
        _isLanding = false;
        if (Input.GetButtonDown("Jump"))
        {
            _isJumping = true;

        }
        if (_isJumping == true)
        {
            if (_jumpTimer < _jumpDuration)
            {
                //_rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                _jumpTimer += Time.deltaTime;

                // Progression / maximum // Donne le pourcentage restant
                float curveY = _jumpCurve.Evaluate(_jumpTimer / _jumpDuration);

                _graphicsTransform.localPosition = new Vector2(_graphicsTransform.localPosition.x, curveY * _jumpHeight);
            }

            else if (_jumpTimer >= _jumpDuration)
            {
                _jumpTimer = 0f;
                _isJumping = false;
                _isLanding = true;
                //_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

        }

    }

    private void Health()
    {
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        if (_health <= 0)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        _healthBar.fillAmount = _health / _maxHealth;
    }

    private void ActivateAnimation()
    {
        float maxValue = Mathf.Max(Mathf.Abs(_direction.x), Mathf.Abs(_direction.y));
        _animator.SetFloat("moveSpeedX", maxValue);
        _animator.SetBool("isJumping", _isJumping);
        _animator.SetBool("isLanding", _isLanding);
        _animator.SetFloat("healthPoints", _health);
        _animator.SetBool("isPickingCanUp", _isPickingCanUp);
        _animator.SetBool("isFighting", _isFighting);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider == _greenCan.GetComponent<BoxCollider2D>())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _isPickingCanUp = true;
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
    bool _isLanding;

    bool _isFighting;
    bool _isPickingCanUp;

    GameObject _enemy;


    #endregion
}
