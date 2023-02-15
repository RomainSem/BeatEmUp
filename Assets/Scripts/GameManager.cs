using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Expose

    [SerializeField] TextMeshProUGUI _scoreTxt;
    
    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        
    }

    void Start()
    {

    }

    void Update()
    {
        ReloadScene();
        _scoreTxt.text = _score.ToString();
    }


    #endregion

    #region Methods

    private void ReloadScene()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    #endregion

    #region Private & Protected

    int _score = 000000;

    public int Score { get => _score; set => _score = value; }

    #endregion
}
