using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMan : MonoBehaviour
{
    #region Singleton

    private static HealthMan _instance = null;

    public static HealthMan Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HealthMan>();

                if (_instance == null)
                {
                    Debug.LogError("Fatal Error: HealthMan not Found");
                }
            }

            return _instance;
        }
    }

    #endregion
    
    /* public int CurrHealth
    {
        get { return _currHealth; }
        set
        {
            _currHealth = value;
            if (_currHealth == 1)
            {
                // DO SOMETHING HERE
                Debug.Log("aaaa");
            }
            Debug.Log("popo");
        }
    } */
    
    [SerializeField] private int _currHealth;
    [SerializeField] private GameObject _hpUI;
    [SerializeField] private ScoreMan _score;


private int _myProperty;
    

    private void Start()
    {
        //_score = ScoreMan.Instance;
        _currHealth = 3;
        UpdateUI();
    } 

    public void DecreaseHealth()
    {
        _currHealth--;
        UpdateUI();
        
        if (_currHealth <= 0) _score.GameOver();

    }

    private void UpdateUI()
    {
        //Debug.Log("HP : " + _currHealth);
        if (_currHealth == 3) 
        {
            _hpUI.transform.GetChild(2).gameObject.SetActive(true);
            _hpUI.transform.GetChild(1).gameObject.SetActive(true);
            _hpUI.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            _hpUI.transform.GetChild(_currHealth).gameObject.SetActive(false);
        }

        //_hpUI.transform.GetChild(2)
    }
}
