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

    private void Start() 
    {
        CurrScore = 0;
        HighScore = PlayerPrefs.GetInt("HighScore");
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
