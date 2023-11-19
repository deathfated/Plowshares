using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    private GridTile _tile;
    private Vector3 _coord;
    private int _occStatus;

    public TileData (GridTile tile, Vector3 coord, int occ)
    {
        _tile = tile;
        _coord = coord;
        _occStatus = occ;

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
