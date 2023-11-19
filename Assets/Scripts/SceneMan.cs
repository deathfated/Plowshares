using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    [SerializeField] private TMP_Text _hiScoreText;
    
    private void Start()
    {
        if (_hiScoreText != null) 
        {
            _hiScoreText.text = "HI-SCORE : " + PlayerPrefs.GetInt("HighScore").ToString();
        }
    }

    public void ToGameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
