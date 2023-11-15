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

    private void Start() 
    {
        CurrScore = 0;
    }

    public void AddScore(int x)
    {
        CurrScore += x;
        _scoreValue.text = CurrScore.ToString();
    }

    private void GameOver()
    {
        if (CurrScore > HighScore) HighScore = CurrScore;
    }
}
