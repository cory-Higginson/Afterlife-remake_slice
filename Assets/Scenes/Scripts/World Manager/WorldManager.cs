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

        planes[0][13].GetComponent<GridLocation>().grid_data.tile_type = TileType.Road;
        planes[0][18].GetComponent<GridLocation>().grid_data.tile_type = TileType.Road;
        planes[0][6].GetComponent<GridLocation>().grid_data.tile_type = TileType.Road;
        planes[0][12].GetComponent<GridLocation>().grid_data.tile_type = TileType.Gate;

        //planes[0][21].GetComponent<GridLocation>().grid_data.tile_type = TileType.Transport;
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

        if (Input.GetKeyDown("a"))
        {
            var test = check_cardinal(planes[0][12], TileType.Road, 0);
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

    private bool pathfinding(GameObject gate, GameObject building, int plane)
    {
        Vector2 start = gate.GetComponent<GridLocation>().grid_data.position;

        Vector2 end = building.GetComponent<GridLocation>().grid_data.position;

        if (start == end) { return true; }
        //Debug.Log("Astar " + AStar(start, end, plane));
        return AStar(start, end, plane);
    }

    private bool AStar(Vector2 start, Vector2 end, int plane)
    {
        List<Vector2> closed_list = new List<Vector2>();

        Queue<Vector2> open_list = new Queue<Vector2>();
        open_list.Enqueue(start);

        while (open_list.Count > 0)
        {
            Vector2 current = open_list.Dequeue();
            if (current == end) { return true; }

            foreach (Vector2 neighbour in getNeighbours(current, end, plane))
            {
                if (closed_list.Contains(current)) { continue; }

                if (neighbour == end) { return true; }

                open_list.Enqueue(neighbour);
            }
            closed_list.Add(current);
        }

        if (closed_list.Contains(end)) { return true; }

        return false;

    }

    private List<Vector2> getNeighbours(Vector2 start, Vector2 end, int plane)
    {
        Debug.Log("checking_for_neighbours");
        List<Vector2> neighbours = new List<Vector2>();

        Vector2 [] directions = new Vector2[8]
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),
            new Vector2(0, 1),
            new Vector2(0, -1),
            new Vector2(1, 1),
            new Vector2(1, -1),
            new Vector2(-1, 1),
            new Vector2(-1, -1)
        };

        foreach (Vector2 dir in directions)
        {
            Vector2 current = new Vector2(start.x + dir.x, start.y + dir.y);
            if (!withinRange(current)) continue;
            
            int tile_index = getIndex(current);

            if (current == end)
            { neighbours.Clear(); Debug.Log("OVER " + current); neighbours.Add(current); return neighbours; }

            if (planes[plane][tile_index].GetComponent<GridLocation>().grid_data.tile_type 
                != TileType.Road) continue;
            neighbours.Add(current);
        }
        Debug.Log(neighbours);
        Debug.Log(neighbours[0]);
        foreach (Vector2 neighbour in neighbours)
        { 

            Debug.Log("AFTER " + neighbour);
        }

        return neighbours;
    }

    private List<Direction> check_cardinal(GameObject obj, TileType checking_for, int plane)
    {
        // Check the cardinal directions of a GameObject for a specific tile
        // Will be used to check if buildings are connected to roads, ect

        Vector2 start = obj.GetComponent<GridLocation>().grid_data.position;

        List<Direction> is_connected = new List<Direction>();

        Vector2[] directions = new Vector2[4]
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

            if (planes[plane][getIndex(current)].GetComponent<GridLocation>().grid_data.tile_type
                == checking_for)
            {
                switch (dir)
                {
                    case Vector2 v when v.Equals(Vector2.right):
                        is_connected.Add(Direction.East);
                        break;
                    case Vector2 v when v.Equals(Vector2.left):
                        is_connected.Add(Direction.West);
                        break;
                    case Vector2 v when v.Equals(Vector2.up):
                        is_connected.Add(Direction.North);
                        break;
                    case Vector2 v when v.Equals(Vector2.down):
                        is_connected.Add(Direction.South);
                        break;
                }
            }
        }

        return is_connected;
    }
}
