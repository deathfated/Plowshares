using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

public class GridTile : MonoBehaviour
{
    
    [Header("Base")]
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _offsetColor;

    [Header("Tile Type")]
    [SerializeField] private int _occupiedStatus;
    [SerializeField] private Color _colorA;
    [SerializeField] private Color _colorB;
    [SerializeField] private Color _colorC;
    [SerializeField] private bool _isThreatened;
    

    private SpriteRenderer _renderer;
    private GameObject _highlight;
    private GameObject _occupySprite;

    //private ScoreMan _score;
    private TileInventory _inv;
    private HealthMan _hp;
    private GridMan _grid;


    private void Awake() 
    {
        _renderer = this.GetComponent<SpriteRenderer>();
        _highlight = this.transform.GetChild(0).gameObject;
        _occupySprite = this.transform.GetChild(1).gameObject;

        //_score = ScoreMan.Instance;
        _inv = TileInventory.Instance;
        _hp = HealthMan.Instance;
        _grid = GridMan.Instance;

        // 0 empty, 1 A Rook, 2 B Knight, 3 C Bishop
        _occupiedStatus = 0; 

    }

    public void Init(bool isOffset)
    {
        _renderer = this.GetComponent<SpriteRenderer>();  
        
        if (isOffset) _renderer.color = _offsetColor;
        else _renderer.color = _baseColor;

    }

    private void OnMouseEnter() 
    {
        _highlight.SetActive(true);

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
        int type = _inv.CurrTileType;
        
        if (_occupiedStatus != 0 || _isThreatened)
        {
            Debug.Log("illegal move!");
            _hp.DecreaseHealth();

        }
        else
        {
            _occupiedStatus = type;
            switch(type)
            {
                case 1:
                    _occupySprite.GetComponent<SpriteRenderer>().color = _colorA;
                    break;
                case 2:
                    _occupySprite.GetComponent<SpriteRenderer>().color = _colorB;
                    break;
                case 3:
                    _occupySprite.GetComponent<SpriteRenderer>().color = _colorC;
                    break;    
            }

            _occupySprite.SetActive(true);

            //_score.AddScore(1);
            _grid.UpdateScore();


            _inv.NextTile();

        }
    }

}
