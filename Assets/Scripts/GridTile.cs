using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridTile : MonoBehaviour
{
    
    [Header("Base")]
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _offsetColor;
    public Vector3 TileCoordinates;

    [Header("Tile Type")]
    public int OccupiedStatus;
    private Color _occupiedColorTemp;
    public int ThreatStatus;
    private bool _isThreatened = false;
    public bool IsThreatened
    {
        get
        {
            return _isThreatened;
        }
        set
        {
            _isThreatened = value;
            //Debug.Log(value);
            //_threatHighlight.SetActive(value);
        }
    }

    private SpriteRenderer _renderer;
    private GameObject _highlight;
    private GameObject _occupySprite;
    public GameObject ThreatHighlight;

    //private ScoreMan _score;
    private TileInventory _inv;
    private HealthMan _hp;
    private GridMan _grid;


    private void Awake() 
    {
        _renderer = this.GetComponent<SpriteRenderer>();
        _highlight = this.transform.GetChild(0).gameObject;
        _occupySprite = this.transform.GetChild(2).gameObject;
        ThreatHighlight = this.transform.GetChild(1).gameObject;

        //_score = ScoreMan.Instance;
        _inv = TileInventory.Instance;
        _hp = HealthMan.Instance;
        _grid = GridMan.Instance;

        // 0 empty, 1 A Rook, 2 B Knight, 3 C Bishop
        OccupiedStatus = 0; 

    }

    public void Init(bool isOffset, int x, int y)
    {
        _renderer = this.GetComponent<SpriteRenderer>();  
        
        if (isOffset) _renderer.color = _offsetColor;
        else _renderer.color = _baseColor;

        TileCoordinates = new Vector3(x,y);

    }

    private void OnMouseEnter() 
    {
        _highlight.SetActive(true);

        int type = _inv.CurrTileType[0];
        _grid.CheckThreat(type, TileCoordinates);

    }

    private void OnMouseExit() 
    {
        _highlight.SetActive(false);
    }

    private void OnMouseDown() 
    {
        //SpriteRenderer sr = _highlight.GetComponent<SpriteRenderer>();
        //Color _highlightBaseColor = sr.color;

        //sr.color =
    }

    private void OnMouseUp() 
    {
        int type = _inv.CurrTileType[0];
        
        if (OccupiedStatus != 0 || IsThreatened)
        {
            Debug.Log("illegal move!");
            _hp.DecreaseHealth();

        }
        else
        {
            _occupySprite.SetActive(true);
            
            OccupiedStatus = type;
            switch(type)
            {
                case 1:
                    _occupiedColorTemp = _renderer.GetComponent<SpriteRenderer>().color;
                    _occupySprite.GetComponent<SpriteRenderer>().color = _inv.ColorA;
                    _grid._tilesA.Add(this);
                    _grid.SpawnThreat(type,TileCoordinates);

                    _grid._tilesOccupied[0]++;
                    if (_grid._tilesOccupied[0] >= 4)
                    {
                        _grid.ResetOccupiedTile(1);
                    }
                    break;
                case 2:
                    _occupiedColorTemp = _renderer.GetComponent<SpriteRenderer>().color;
                    _occupySprite.GetComponent<SpriteRenderer>().color = _inv.ColorB;
                    _grid._tilesB.Add(this);
                    _grid.SpawnThreat(type,TileCoordinates);

                    _grid._tilesOccupied[1]++;
                    if (_grid._tilesOccupied[1] >= 4)
                    {
                        _grid.ResetOccupiedTile(2);
                    }
                    break;
                case 3:
                    _occupiedColorTemp = _renderer.GetComponent<SpriteRenderer>().color;
                    _occupySprite.GetComponent<SpriteRenderer>().color = _inv.ColorC;
                    _grid._tilesC.Add(this);
                    _grid.SpawnThreat(type,TileCoordinates);

                    _grid._tilesOccupied[2]++;
                    if (_grid._tilesOccupied[2] >= 4)
                    {
                        _grid.ResetOccupiedTile(3);
                    }
                    break;    
            }

            _inv.NextTile();

        }
    }

    public void ResetOccupiedColor()
    {
        //_occupySprite.GetComponent<SpriteRenderer>().color = _occupiedColorTemp;
        _occupySprite.SetActive(false);
    }
    
}
