using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLocation : MonoBehaviour
{
    public GameObject generic_building;
    public GameObject road;
    public GameObject gate;

    public GridData grid_data;

    private float timer = 0;
    private float max_timer = 5;

    private Color brown = new Color(0.5188679f, 0.3241828f, 0.007342457f, 1.0f);
    private Color orange = new Color(0.901f, 0.4962393f, 0f, 1.0f);
    private Color purple = new Color(0.754717f, 0.1103596f, 0.5200665f, 1.0f);

    private Material tile_mat;

    // Start is called before the first frame update
    void Start()
    {
        tile_mat = this.gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (!grid_data.connected)
        {
            tile_mat.color = Color.cyan;
        }
        else
        {
            switch (grid_data.tile_type)
            {
                case TileType.Zone:
                    switch (grid_data.zone_type)
                    {
                        case ZoneType.Blue:
                            tile_mat.color = Color.blue;
                            break;
                        case ZoneType.Green:
                            tile_mat.color = Color.green;
                            break;
                        case ZoneType.Yellow:
                            tile_mat.color = Color.yellow;
                            break;
                        case ZoneType.Red:
                            tile_mat.color = Color.red;
                            break;
                        case ZoneType.Brown:
                            tile_mat.color = brown;
                            break;
                        case ZoneType.Generic:
                            tile_mat.color = Color.black;
                            break;
                        case ZoneType.Purple:
                            tile_mat.color = purple;
                            break;
                        case ZoneType.Orange:
                            tile_mat.color = orange;
                            break;
                        case ZoneType.None:
                            tile_mat.color = Color.white;
                            break;
                        default:
                            // Do nothing
                            break;
                    }

                    break;
                case TileType.Road:
                    tile_mat.color = new Color(55, 43, 37, 1);
                    break;
                case TileType.Gate:
                    tile_mat.color = Color.grey;
                    break;
                default:
                    tile_mat.color = Color.white;
                    break;
            }
        }
        

        // TileType.Blue || grid_data.tile_type == TileType.Green || grid_data.tile_type == TileType.Yellow || grid_data.tile_type == TileType.Red
        if (grid_data.tile_type != TileType.None && grid_data.connected)
        {
            timer += Time.deltaTime;
        }

        if (timer >= max_timer && grid_data.stored_building == null)
        {
            Vector3 spawn_point = new Vector3(this.transform.position.x, this.transform.position.y + 0.05f, this.transform.position.z);

            switch (grid_data.tile_type)
            {
                case TileType.Road:
                    grid_data.stored_building = Instantiate(road, spawn_point, Quaternion.identity, this.transform);
                    break;
                case TileType.Gate:
                    grid_data.stored_building = Instantiate(gate, spawn_point, Quaternion.identity, this.transform);
                    grid_data.connected_directions = WorldManager.Instance.check_cardinal(grid_data.stored_building, TileType.Road, 0);
                    if (grid_data.connected_directions.Count > 0)
                    {
                        //grid_data.stored_building.GetComponent<GateScript>().connected = true;
                    }
                    break;
                default:
                    grid_data.stored_building = Instantiate(generic_building, spawn_point, Quaternion.identity, this.transform);
                    break;
            }
        }
    }
}


