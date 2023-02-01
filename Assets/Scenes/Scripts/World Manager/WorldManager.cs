using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject grid_location;

    static public int num_of_planes = 2;

    public int grid_x;
    public int grid_y;

    private GameObject [][] planes = new GameObject[num_of_planes][];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < num_of_planes; i++)
        {
            planes[i] = new GameObject[grid_x * grid_y];
        }

        for (int i = 0; i < num_of_planes; i++)
        {
            for (int j = 0; j < grid_y; j++)
            {
                for (int k = 0; k < grid_x; k++)
                {
                    Vector3 spawn_location = new Vector3(k, i * 2, j);
                    planes[i][j * grid_x + k] = Instantiate(grid_location, spawn_location, Quaternion.identity, this.transform);
                    planes[i][j * grid_x + k].GetComponent<GridLocation>().grid_data =
                        new GridData(TileType.None, new Vector2(k, j));
                }
            }
        }

        planes[0][3].GetComponent<GridLocation>().grid_data.tile_type = TileType.Blue;
        planes[0][4].GetComponent<GridLocation>().grid_data.tile_type = TileType.Blue;
        planes[0][8].GetComponent<GridLocation>().grid_data.tile_type = TileType.Blue;
        planes[0][9].GetComponent<GridLocation>().grid_data.tile_type = TileType.Blue;
        planes[0][20].GetComponent<GridLocation>().grid_data.tile_type = TileType.Yellow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
