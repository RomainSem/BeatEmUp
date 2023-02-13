using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnnemyHealth : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private int _ennemyHealth;

    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Nombre de vie : " + _ennemyHealth);
            _ennemyHealth --;
            if (_ennemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    #region Private

    Rigidbody2D _rigidbody;

    #endregion
}
