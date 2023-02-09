using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    public TileType tile_type;
    public GameObject stored_building;
    public Vector2 position;
    public List<KeyValuePair<Direction, TileType>> connected_directions;

    public GridData(TileType _tile_type, Vector2 _position)
    {
        tile_type = _tile_type;
        position = _position;
    }
}
