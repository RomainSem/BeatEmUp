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
        _enemy = GameObject.FindGameObjectWithTag("Ennemy1");
        _enemyCollider = _enemy.GetComponentInChildren<Collider2D>();
        _enemyAnimator = _enemyCollider.GetComponent<Animator>();


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
            //_enemy.GetComponent<Enemy>().Health--;
            _enemyAnimator.SetTrigger("isHurting");
            _enemy.GetComponent<Enemy>().TakeDamage();
            Debug.Log("Enemy Enter");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy1"))
        {
            Debug.Log("EXIT");
        }
    }

    #endregion

    #region Private & Protected

    GameObject _enemy;
    Collider2D _enemyCollider;
    bool _isEnemyHurt;
    Animator _enemyAnimator;

    #endregion
}
