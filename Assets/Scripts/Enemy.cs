using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region Expose

    [SerializeField] float _health = 5f;
    [SerializeField] float _maxHealth = 5f;
    [SerializeField] byte _damage = 1;


    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("PlayerGraphics");
        _playerFistCollider = _player.GetComponent<Collider2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        Life();
        Debug.Log(_health);
    }


    #endregion

    #region Methods

    private void Life()
    {
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        if (_health <= 0)
        {
            _animator.SetBool("isDead", true);
            gameObject.GetComponent<EnnemyBehaviour>().enabled = false;
            gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }

    #endregion

    #region Private & Protected

    Rigidbody2D _rigidbody;
    Collider2D _playerFistCollider;
    Animator _animator;
    GameObject _player;

    public byte Damage { get => _damage; set => _damage = value; }
    public float Health { get => _health; set => _health = value; }


    #endregion
}
