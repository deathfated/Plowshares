using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetThreatStatus(int status)
    {
        _threatStatus = status;
    }

    public GridTile GetTile()
    {
        return _tile;
    }
    public Vector3 GetCoord()
    {
        return _coord;
    }
    public int GetThreatStatus()
    {
        return _threatStatus;
    }
}
