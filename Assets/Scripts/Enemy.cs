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
    [SerializeField] int _scoreGiven = 20;


    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerGraphics");
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        Life();
    }


    #endregion

    #region Methods

    private void Life()
    {
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    public void TakeDamage()
    {
        _health -= _player.GetComponentInParent<PlayerMovement>().Damage;
        if (_health > 0)
        {
            _animator.SetTrigger("isHurting");
            Debug.Log(_health);
        }
        else if (_health <= 0)
        {
            _animator.SetBool("isDead", true);
            gameObject.GetComponent<EnnemyBehaviour>().enabled = false;
            gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
            StartCoroutine(EnemyDeath());
            GameObject.Find("GameManager").GetComponent<GameManager>().Score += _scoreGiven;
        }
    }

    IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.2f);
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
        Destroy(gameObject);
    }

    #endregion

    #region Private & Protected

    Animator _animator;
    GameObject _player;

    public byte Damage { get => _damage; set => _damage = value; }
    public float Health { get => _health; set => _health = value; }

    #endregion
}
