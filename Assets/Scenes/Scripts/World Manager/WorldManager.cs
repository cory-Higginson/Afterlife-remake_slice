using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.Search;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject grid_location;

    static public int num_of_planes = 2;

    public int grid_x;
    public int grid_y;

    private GameObject [][] planes = new GameObject[num_of_planes][];

    public Vector3 center_point;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < num_of_planes; i++)
        {
            planes[i] = new GameObject[grid_x * grid_y];
        }

        center_point = new Vector3(grid_x / 2 - 0.5f, 0, grid_y / 2 - 0.5f);

        for (int i = 0; i < num_of_planes; i++)
        {
            for (int j = 0; j < grid_y; j++)
            {
                for (int k = 0; k < grid_x; k++)
                {
                    Vector3 spawn_location = new Vector3(k, i * 3, j);
                    planes[i][j * grid_x + k] = Instantiate(grid_location, spawn_location, Quaternion.identity, this.transform);
                    planes[i][j * grid_x + k].GetComponent<GridLocation>().grid_data =
                        new GridData(TileType.None, new Vector2(k, j));
                }
            }
        }

        planes[0][3].GetComponent<GridLocation>().grid_data.tile_type = TileType.Blue;
        planes[0][4].GetComponent<GridLocation>().grid_data.tile_type = TileType.Blue;
        planes[0][9].GetComponent<GridLocation>().grid_data.tile_type = TileType.Blue;
        planes[0][10].GetComponent<GridLocation>().grid_data.tile_type = TileType.Blue;
        planes[0][20].GetComponent<GridLocation>().grid_data.tile_type = TileType.Yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("]"))
        {
            Debug.Log("works?");
            foreach (GameObject[] plane in planes)
            {
                foreach (GameObject tile in plane)
                {
                    tile.transform.RotateAround(center_point, Vector3.up, 90);
                }
            }
        }
        if (Input.GetKeyDown("["))
        {
            Debug.Log("works?");
            foreach (GameObject[] plane in planes)
            {
                foreach (GameObject tile in plane)
                {
                    tile.transform.RotateAround(center_point, Vector3.up, -90);
                }
            }
        }
    }

    private int getIndex(Vector2 position)
    {
        return (int)position.x + (int)position.y * grid_x;
    }

    private bool withinRange(Vector2 position)
    {
        if (position.x > grid_x - 1 || position.x < 0 ||
            position.y > grid_y - 1 || position.y < 0) return false;
        return true;
    }

    private void pathfinding(GameObject gate, GameObject building, int plane)
    {
        Vector2 start = gate.GetComponent<GridLocation>().grid_data.position;

        Vector2 end = building.GetComponent<GridLocation>().grid_data.position;


    }

    private void AStar()
    {

    }

    private GameObject[] getNeighbours(Vector2 start, int plane)
    {
        GameObject[] neighbours = new GameObject[4];

        Vector2 [] directions = new Vector2[4]
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),
            new Vector2(0, 1),
            new Vector2(0, -1)
        };

        foreach (Vector2 dir in directions)
        {
            Vector2 current = new Vector2(start.x + dir.x, start.y + dir.y);

            if (!withinRange(current)) continue;

            int tile_index = getIndex(current);

            if (planes[0][tile_index].GetComponent<GridLocation>().grid_data.tile_type 
                != TileType.Transport) continue;

            neighbours.Append(planes[plane][tile_index]);
        }

        return neighbours;
    }
}
