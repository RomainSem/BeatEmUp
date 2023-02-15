using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphics : MonoBehaviour
{
    #region Expose

    
    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _enemy = GameObject.FindGameObjectWithTag("Ennemy1");
        _enemyCollider = _enemy.GetComponentInChildren<Collider2D>();
        _enemyRGBody = _enemy.GetComponent<Rigidbody2D>();
        _enemyAnimator = _enemyCollider.GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        //ActivateAnimations();
    }


    #endregion

    #region Methods

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision == _enemyCollider)
    //    {
    //        //_isEnemyHurt = true;
    //        //_enemy.GetComponent<Enemy>().Health--;
    //        //_enemyAnimator.SetTrigger("isHurting");
    //        _enemy.GetComponent<Enemy>().TakeDamage();
    //        Debug.Log("ENTER");

    //    }

    //}

    #endregion

    #region Private & Protected

    GameObject _enemy;
    Collider2D _enemyCollider;
    Rigidbody2D _enemyRGBody;
    Animator _enemyAnimator;
    bool _isEnemyHurt;

    #endregion
}
