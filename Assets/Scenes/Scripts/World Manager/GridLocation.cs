using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLocation : MonoBehaviour
{
    public GridData grid_data;

    public GameObject generic_building;
    public GameObject road;
    public GameObject gate;

    private float timer = 0;
    private float max_timer = 5;

    private Material tile_mat;

    // Start is called before the first frame update
    void Start()
    {
        tile_mat = this.gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        switch (grid_data.tile_type)
        {
            case TileType.Blue:
                tile_mat.color = Color.blue;
                break;
            case TileType.Green:
                tile_mat.color = Color.green;
                break;
            case TileType.Yellow:
                tile_mat.color = Color.yellow;
                break;
            case TileType.Red:
                tile_mat.color = Color.red;
                break;
            case TileType.Road:
                tile_mat.color = Color.black;
                break;
            case TileType.Gate:
                tile_mat.color = Color.grey;
                break;
            default:
                tile_mat.color = Color.white;
                break;
        }

        // TileType.Blue || grid_data.tile_type == TileType.Green || grid_data.tile_type == TileType.Yellow || grid_data.tile_type == TileType.Red
        if (grid_data.tile_type != TileType.None)
        {
            timer += Time.deltaTime;
        }

        if (timer >= max_timer && grid_data.stored_building == null)
        {
            Vector3 spawn_point = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);

            switch (grid_data.tile_type)
            {
                case TileType.Road:
                    grid_data.stored_building = Instantiate(road, spawn_point, Quaternion.identity, this.transform);
                    break;
                case TileType.Gate:
                    grid_data.stored_building = Instantiate(gate, spawn_point, Quaternion.identity, this.transform);
                    break;
                default:
                    grid_data.stored_building = Instantiate(generic_building, spawn_point, Quaternion.identity, this.transform);
                    break;
            }

            
        }
    }
}


