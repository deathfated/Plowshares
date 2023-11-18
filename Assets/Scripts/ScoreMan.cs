using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMan : MonoBehaviour
{
    public int CurrScore;
    public int HighScore;
    [SerializeField] private TMP_Text _scoreValue;

    [SerializeField] private GameObject _gameOverPanel;


    #region Singleton

    private static ScoreMan _instance = null;

    /* public static ScoreMan Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreMan>();

                if (_instance == null)
                {
                    Debug.LogError("Fatal Error: ScoreManager not Found");
                }
            }

            return _instance;
        }
    } */

    #endregion


    private void Start() 
    {
        CurrScore = 0;
        HighScore = PlayerPrefs.GetInt("HighScore");
        Debug.Log(HighScore);
    }

    public void AddScore(int x)
    {
        CurrScore += x;
        _scoreValue.text = CurrScore.ToString();
        //Debug.Log(CurrScore);
    }

    public void GameOver()
    {
        Debug.Log("GameOver " + CurrScore);

        _gameOverPanel.SetActive(true);
        _gameOverPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = "SCORE : "+ CurrScore;

        if (CurrScore >= HighScore) 
        {
            PlayerPrefs.SetInt("HighScore", CurrScore);
            
        }
    }
}
