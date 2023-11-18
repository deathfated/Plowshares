using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridMan : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private GridTile _tilePrefab;
    [SerializeField] Transform _camera;

    private Dictionary<Vector2, Tile> _tiles;
    [SerializeField] private ScoreMan _score;

    private bool isGameOver;


    #region Singleton

    private static GridMan _instance = null;

    public static GridMan Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GridMan>();

                if (_instance == null)
                {
                    Debug.LogError("Fatal Error: GridManager not Found");
                }
            }

            return _instance;
        }
    }

    #endregion


    private void Start() 
    {
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x,y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                //var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                var isOffset = ((x + y) % 2 == 1);
                spawnedTile.Init(isOffset);
            }
        }

        _camera.transform.position = new Vector3((float)_width/2 - 0.5f, (float)_height/2  - 0.5f, -10);
        //_camera.transform.position = new Vector3((float)_width/2 + 1.0f, (float)_height/2  -0f, -10);
    }

    public Tile GetTile(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;

    }

    public void UpdateScore()
    {
        _score.AddScore(1);
    }

}
