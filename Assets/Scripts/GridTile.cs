using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _offsetColor;

    private GameObject _highlight;
    private SpriteRenderer _renderer;
    [SerializeField] private ScoreMan _score;


    private void Awake() 
    {
        _renderer = this.GetComponent<SpriteRenderer>();
        _highlight = this.transform.GetChild(0).gameObject;
        _score = FindObjectOfType<ScoreMan>();

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
        _score.AddScore(1);
    }

}
