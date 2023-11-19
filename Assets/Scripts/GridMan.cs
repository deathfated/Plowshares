using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class GridMan : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] Transform _camera;
    [SerializeField] private ScoreMan _score;

    [Header("Data")]
    [SerializeField] private GridTile _tilePrefab;

    private List<TileData> _tileData;

    public List<GridTile> _tilesA, _tilesB, _tilesC;
    public List<GridTile> _threatA, _threatB, _threatC;
    public List<int> _tilesOccupied;


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

        _tilesOccupied.Add(0); //1 2 3 4 
        _tilesOccupied.Add(0);
        _tilesOccupied.Add(0);

    }

    private void SpawnGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var v = new Vector3(x,y);
                var spawnedTile = Instantiate(_tilePrefab, v, UnityEngine.Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                //_tileData.Add(new TileData(spawnedTile, v, 0, 0));

                var isOffset = ((x + y) % 2 == 1);
                spawnedTile.Init(isOffset, x ,y);
            }
        }

        _camera.transform.position = new Vector3((float)_width/2 - 0.5f, (float)_height/2  - 0.5f, -10);
    }

    public void ResetOccupiedTile(int type)
    {
        switch(type)
        {
            case 0:
                for (int a = 0 ; a < _tilesA.Count; a++)
                {
                    _tilesA[a].ResetOccupiedColor();
                    _tilesA[a].OccupiedStatus = 0;
                }

                _score.AddScore(5);
                _tilesOccupied[0] = 0; 
                return;
            case 1:
                for (int a = 0 ; a < _tilesB.Count; a++)
                {
                    _tilesB[a].ResetOccupiedColor(); 
                    _tilesB[a].OccupiedStatus = 0;
                }

                _score.AddScore(10);
                _tilesOccupied[1] = 0;
                return;
            case 2:
                for (int a = 0 ; a < _tilesC.Count; a++)
                {
                    _tilesC[a].ResetOccupiedColor();
                    _tilesC[a].OccupiedStatus = 0; 
                }

                _score.AddScore(12);
                _tilesOccupied[2] = 0;
                return;
        }
    }

    public void SpawnThreat(int type, Vector3 coord)
    {
        switch(type)
        {
            case 0: //Rook
                /* for (int a = 0; a < _tileData.Count; a++)
                {
                    var cor = _tileData[a].GetCoord();

                    if (cor.x == coord.x && cor.x <= _width && cor.x > 0)
                    {
                        GridTile ti = _tileData[a].GetTile();
                        ti.IsThreatened = true;

                    }
                    if (cor.y == coord.y&& cor.y <= _height && cor.y > 0)
                    {

                    }

                }  */

                return;
            case 1: //Knight

                return;
            case 2: //BiSHOP

                return;
        }
    }
}

public class TileData
{
    private GridTile _tile;
    private Vector3 _coord;
    private int _occStatus;
    private int _threatStatus;

    public TileData (GridTile tile, Vector3 coord, int occ, int threat)
    {
        _tile = tile;
        _coord = coord;
        _occStatus = occ;
        _threatStatus = threat;

    }
    public GridTile GetTile()
    {
        return _tile;
    }
    public Vector3 GetCoord()
    {
        return _coord;
    }
}
