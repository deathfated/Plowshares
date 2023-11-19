using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInventory : MonoBehaviour
{
    public List<int> CurrTileType;
    public List<int> CurrInventory;
    [SerializeField] private GameObject _invPanel; 
    public Color ColorA;
    public Color ColorB;
    public Color ColorC;

    
    #region Singleton

    private static TileInventory _instance = null;

    public static TileInventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TileInventory>();

                if (_instance == null)
                {
                    Debug.LogError("Fatal Error: TileInventory not Found");
                }
            }

            return _instance;
        }
    }

    #endregion


    private void Start() 
    {
        CurrTileType.Add(Random.Range(1,4));
        CurrTileType.Add(Random.Range(1,4));
        CurrTileType.Add(Random.Range(1,4));
        CurrTileType.Add(Random.Range(1,4));

        UpdateUI();
        
        //NextTile();
    } 

    public void NextTile()
    {
        //CurrTileType = Random.Range(1,4);           //1 2 3
        //Debug.Log("Current Tile : " + CurrTileType);

        CurrTileType.Add(Random.Range(1,4));
        CurrTileType.RemoveAt(0);

        UpdateUI();
    }

    private void UpdateUI()
    {
        Color setColor = Color.black;
        for (int i = 0; i < 4 ; i++)
        {
            if (CurrTileType[i] == 1) setColor = ColorA;
            else if (CurrTileType[i] == 2) setColor = ColorB;
            else if (CurrTileType[i] == 3) setColor = ColorC;
            /* switch (CurrTileType[i])
            {
                case 1:
                    setColor = ColorA;
                    return;
                case 2:
                    setColor = ColorB;
                    return;
                case 3:
                    setColor = ColorC;
                    return;
            } */

            int a = 0;
            if (i == 0) a = 3;
            else if (i == 1) a = 2;
            else if (i == 2) a = 1;
            else if (i == 3) a = 0;
            _invPanel.transform.GetChild(a).GetComponent<Image>().color = setColor;
        }
    }
}
