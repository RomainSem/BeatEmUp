using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistCollider : MonoBehaviour
{
    #region Expose


    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _enemy = GameObject.FindGameObjectWithTag("Ennemy1");
        _enemyCollider = _enemy.GetComponentInChildren<Collider2D>();
        _enemyAnimator = _enemy.GetComponentInChildren<Animator>();


    }

    void Start()
    {

    }

    void Update()
    {

    }


    #endregion

    #region Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTER1");
        if (collision.CompareTag("Ennemy1"))
        {
            _isEnemyHurt = true;
            if (_isEnemyHurt)
            {
                _enemyAnimator.SetTrigger("isHurting");
                _enemy.GetComponent<Enemy>().TakeDamage();
                Debug.Log("Enemy Enter");
            }
            //_enemy.GetComponent<Enemy>().Health--;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isEnemyHurt = false;
        if (collision.CompareTag("Ennemy1"))
        {
            if (_isEnemyHurt == false)
            {
                Debug.Log("EXIT");
            }
        }
    }

    #endregion

    #region Private & Protected

    GameObject _enemy;
    Collider2D _enemyCollider;
    bool _isEnemyHurt;
    Animator _enemyAnimator;
    BoxCollider2D _collider;
    #endregion
}
