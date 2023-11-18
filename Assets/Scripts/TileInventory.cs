using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInventory : MonoBehaviour
{
    public int CurrTileType;


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
        NextTile();
    } 

    public void NextTile()
    {
        CurrTileType = Random.Range(1,4);
        Debug.Log("Current Tile : " + CurrTileType);
    }
}
