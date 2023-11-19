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

    private List<TileData> _tileData = new List<TileData>();

    public List<GridTile> _tilesA, _tilesB, _tilesC;
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

                _tileData.Add(new TileData(spawnedTile, v, 0));

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
            case 1:
                for (int a = 0 ; a < _tilesA.Count; a++)
                {
                    _tilesA[a].ResetOccupiedColor();
                    _tilesA[a].OccupiedStatus = 0;
                }

                _tilesOccupied[0] = 0;
                _tilesA.Clear();
                ResetThreatTiles(1);
                _score.AddScore(5);
                break;
            case 2:
                for (int a = 0 ; a < _tilesB.Count; a++)
                {
                    _tilesB[a].ResetOccupiedColor(); 
                    _tilesB[a].OccupiedStatus = 0;
                    
                }

                _tilesOccupied[1] = 0;
                _tilesB.Clear();
                ResetThreatTiles(2);
                _score.AddScore(10);
                break;
            case 3:
                for (int a = 0 ; a < _tilesC.Count; a++)
                {
                    _tilesC[a].ResetOccupiedColor();
                    _tilesC[a].OccupiedStatus = 0; 
                    
                }

                _tilesOccupied[2] = 0;
                _tilesC.Clear();
                ResetThreatTiles(3);
                _score.AddScore(12);
                break;
        }
        //_tilesOccupied[type] = 0;
        //ResetThreatTiles(type);

    }

    public void CheckThreat(int type, Vector3 coord)
    {
        if (type == 1) // ROOK
        {
            for (int i = 0; i < _tileData.Count; i++)
            {
                _tileData[i].GetTile().ThreatHighlight.SetActive(false);

                var cor = _tileData[i].GetCoord();

                if (cor.x == coord.x && cor.x <= _width && cor.x >= 0)
                {
                    GridTile ti = _tileData[i].GetTile();
                    ti.ThreatHighlight.SetActive(true);

                }
                else if (cor.y == coord.y && cor.y <= _height && cor.y >= 0)
                {
                    GridTile ti = _tileData[i].GetTile();
                    ti.IsThreatened = true;
                    ti.ThreatHighlight.SetActive(true);
                }

            } 
        }
        else if (type == 2) // KNIGHT
        {
            for (int i = 0; i < _tileData.Count; i++)
            {
                _tileData[i].GetTile().ThreatHighlight.SetActive(false);
                
                var cor = _tileData[i].GetCoord();

                if ((cor.x == coord.x + 1 || cor.x == coord.x - 1) && cor.x <= _width && cor.x >= 0)
                {
                    if((cor.y == coord.y + 2 || cor.y == coord.y - 2) && cor.y <= _height && cor.y >= 0)
                    {
                        GridTile ti = _tileData[i].GetTile();
                        ti.ThreatHighlight.SetActive(true);
                    }
                } 
                else if ((cor.x == coord.x + 2 || cor.x == coord.x - 2) && cor.x <= _width && cor.x >= 0)
                {
                    if((cor.y == coord.y + 1 || cor.y == coord.y - 1) && cor.y <= _height && cor.y >= 0)
                    {
                        GridTile ti = _tileData[i].GetTile();
                        ti.ThreatHighlight.SetActive(true);
                    }
                }
            }
        }
        else if (type == 3) //BISHOP
        {
            for (int i = 0; i < _tileData.Count; i++)
            {
                _tileData[i].GetTile().ThreatHighlight.SetActive(false);

                var cor = _tileData[i].GetCoord();

                for (int d = 0; d < _width; d++) //hrs simetris gridnya
                {
                    if ((cor.x == coord.x + d || cor.x == coord.x - d) && cor.x <= _width && cor.x >= 0)
                    {
                        if ((cor.y == coord.y + d || cor.y == coord.y - d) && cor.y <= _height && cor.y >= 0)
                        {
                            GridTile ti = _tileData[i].GetTile();
                            ti.ThreatHighlight.SetActive(true);
                        }
                    }
                } 
                
            }
        }
    }

    public void SpawnThreat(int type, Vector3 coord)
    {
        if (type == 1) // ROOK
        {
            for (int i = 0; i < _tileData.Count; i++)
            {
                var cor = _tileData[i].GetCoord();

                if (cor.x == coord.x && cor.x <= _width && cor.x >= 0)
                {
                    GridTile ti = _tileData[i].GetTile();
                    ti.IsThreatened = true;
                    ti.ThreatStatus = type;

                }
                else if (cor.y == coord.y && cor.y <= _height && cor.y >= 0)
                {
                    GridTile ti = _tileData[i].GetTile();
                    ti.IsThreatened = true;
                    ti.ThreatStatus = type;
                }

            } 
        }
        else if (type == 2) // KNIGHT
        {
            for (int i = 0; i < _tileData.Count; i++)
            {
                var cor = _tileData[i].GetCoord();

                if ((cor.x == coord.x + 1 || cor.x == coord.x - 1) && cor.x <= _width && cor.x >= 0)
                {
                    if((cor.y == coord.y + 2 || cor.y == coord.y - 2) && cor.y <= _height && cor.y >= 0)
                    {
                        GridTile ti = _tileData[i].GetTile();
                        ti.IsThreatened = true;
                        ti.ThreatStatus = type;
                    }
                } 
                else if ((cor.x == coord.x + 2 || cor.x == coord.x - 2) && cor.x <= _width && cor.x >= 0)
                {
                    if((cor.y == coord.y + 1 || cor.y == coord.y - 1) && cor.y <= _height && cor.y >= 0)
                    {
                        GridTile ti = _tileData[i].GetTile();
                        ti.IsThreatened = true;
                        ti.ThreatStatus = type;
                    }
                }
            }
        }
        else if (type == 3) //BISHOP
        {
            for (int i = 0; i < _tileData.Count; i++)
            {
                var cor = _tileData[i].GetCoord();

                for (int d = 0; d < _width; d++) //hrs simetris gridnya
                {
                    if ((cor.x == coord.x + d || cor.x == coord.x - d) && cor.x <= _width && cor.x >= 0)
                    {
                        if ((cor.y == coord.y + d || cor.y == coord.y - d) && cor.y <= _height && cor.y >= 0)
                        {
                            GridTile ti = _tileData[i].GetTile();
                            ti.IsThreatened = true;
                            ti.ThreatStatus = type;
                        }
                    }
                } 
                
            }
        }
    }

    private void ResetThreatTiles(int type)
    {
        for (int a = 0; a < _tileData.Count; a++)
            {
                //if (_tileData[a].GetThreatStatus() == type)
                if (_tileData[a].GetTile().ThreatStatus == type)
                {
                    _tileData[a].GetTile().IsThreatened = false;
                    //tile.IsThreatened = false;
                }

            } 
    }
}
