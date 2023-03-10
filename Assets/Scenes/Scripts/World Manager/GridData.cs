using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridData
{
    public TileType tile_type = TileType.None;
    public ZoneType zone_type = ZoneType.None;
    public GameObject stored_building;
    public Vector2 position = new Vector2(0, 0);
    public List<Direction> connected_directions;
    public bool connected;
    public int plane;
    public int vibes = 0;

    public GridData(TileType _tile_type, Vector2 _position, int _plane)
    {
        tile_type = _tile_type;
        position = _position;
        plane = _plane;
    }
}
