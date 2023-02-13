using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldManager : Singleton<WorldManager>
{
    public GameObject grid_location;
    
    // List of Grid_Location GameObjects
    public List<GameObject> grid_location_list;
    public int amount_of_changed_tiles;

    static public int num_of_planes = 2;

    public int grid_x;
    public int grid_y;

    public GameObject [][] planes = new GameObject[num_of_planes][];

    public Vector3 center_point;

    // Start is called before the first frame update
     protected override void  Awake()
    {
        base.Awake();
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
                    Vector3 spawn_location = new Vector3(k, i * 5, j);
                    planes[i][j * grid_x + k] = Instantiate(grid_location, spawn_location, Quaternion.identity, this.transform);
                    planes[i][j * grid_x + k].GetComponent<GridLocation>().grid_data =
                        new GridData(TileType.None, new Vector2(k, j), i);
                }
            }
        }
        amount_of_changed_tiles = grid_location_list.Count;

        planes[0][3].GetComponent<GridLocation>().grid_data.tile_type = TileType.Zone;
        planes[0][3].GetComponent<GridLocation>().grid_data.zone_type = ZoneType.Blue;

        planes[0][4].GetComponent<GridLocation>().grid_data.tile_type = TileType.Zone;
        planes[0][4].GetComponent<GridLocation>().grid_data.zone_type = ZoneType.Blue;

        planes[0][9].GetComponent<GridLocation>().grid_data.tile_type = TileType.Zone;
        planes[0][9].GetComponent<GridLocation>().grid_data.zone_type = ZoneType.Blue;

        planes[0][10].GetComponent<GridLocation>().grid_data.tile_type = TileType.Zone;
        planes[0][10].GetComponent<GridLocation>().grid_data.zone_type = ZoneType.Blue;

        planes[0][20].GetComponent<GridLocation>().grid_data.tile_type = TileType.Zone;
        planes[0][20].GetComponent<GridLocation>().grid_data.zone_type = ZoneType.Yellow;

        planes[0][13].GetComponent<GridLocation>().grid_data.tile_type = TileType.Road;
        setConnected(planes[0][13], 0);

        planes[0][18].GetComponent<GridLocation>().grid_data.tile_type = TileType.Road;
        setConnected(planes[0][18], 0);

        planes[0][6].GetComponent<GridLocation>().grid_data.tile_type = TileType.Road;
        setConnected(planes[0][6], 0);

        planes[0][12].GetComponent<GridLocation>().grid_data.tile_type = TileType.Gate;

        //planes[0][21].GetComponent<GridLocation>().grid_data.tile_type = TileType.Transport;
    }

     private void Start()
     {
         InputManager.Instance.my_input_actions.AfterLifeActions.RotateView.started += rotateView;
     }

     // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            var test = check_cardinal(planes[0][12], TileType.Road, 0);

            //var temp = check_cardinal(SOUL, TileType.Road, 0);
        }
    }

    private void rotateView(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Single>() == 1)
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
        else
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

    public ref GameObject[][] getPlanes()
    {
        Debug.Log(planes[0].Length);
        return ref planes;
    }
    
    public int getIndex(Vector2 position)
    {
        return (int)position.x + (int)position.y * grid_x;
    }

    public bool withinRange(Vector2 position)
    {
        if (position.x > grid_x - 1 || position.x < 0 ||
            position.y > grid_y - 1 || position.y < 0) return false;
        return true;
    }

    /*
    public List<GridLocation> pathfinding(GridLocation start, GridLocation end, int plane)
    {
        Vector2 start_point = start.grid_data.position;

        Vector2 end_point = end.grid_data.position;

        if (start == end) { return null; }
        
        List<Vector2> path = AStar(start_point, end_point, plane);
        List<GridLocation> tile_path = new List<GridLocation>();

        foreach (var point in path)
        {
            tile_path.Add(planes[plane][getIndex(point)].GetComponent<GridLocation>());
        }

        return tile_path;
    }

    /*
    private List<Vector2> AStar(Vector2 start, Vector2 end, int plane)
    {
        Dictionary<Vector2, Vector2> closed_list = new Dictionary<Vector2, Vector2>();
        
        PriorityQueue<string, int> queue = new PriorityQueue<string, int>();
        PriorityQueue<Vector2, Vector2> open_list = new Queue<Vector2>();
        Dictionary<Vector2, int> costs_so_far = new Dictionary<Vector2, int>();
        costs_so_far[start] = 0;
        
        open_list.Enqueue(start);
        closed_list.Add(start, start);

        while (open_list.Count > 0)
        {
            Vector2 current = open_list.Dequeue();
            if (current == end)
            {
                break;
            }

            foreach (Vector2 neighbour in getNeighbours(current, end, plane))
            {
                int step_cost = costs_so_far[current] + 1;
                
                if (closed_list.Contains(current)) { continue;}

                if (neighbour == end)
                {
                    costs_so_far[neighbour] = step_cost;
                    int priority = 
                    closed_list.Add(neighbour);
                    return closed_list;
                }

                open_list.Enqueue(neighbour);
            }
            
            closed_list.Add(current);
        }

        if (closed_list.Contains(end)) { return closed_list; }

        return null;

    }
    */
/*
    private List<Vector2> getNeighbours(Vector2 start, Vector2 end, int plane)
    {
        Debug.Log("checking_for_neighbours");
        List<Vector2> neighbours = new List<Vector2>();

        Vector2 [] directions = new Vector2[4]
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),
            new Vector2(0, 1),
            new Vector2(0, -1)
            //new Vector2(1, 1),
            //new Vector2(1, -1),
            //new Vector2(-1, 1),
            //new Vector2(-1, -1)
        };

        foreach (Vector2 dir in directions)
        {
            Vector2 current = new Vector2(start.x + dir.x, start.y + dir.y);
            if (!withinRange(current)) continue;
            
            int tile_index = getIndex(current);

            if (current == end)
            {
                neighbours.Clear();
                neighbours.Add(current); 
                return neighbours;
            }

            /*
            if (planes[plane][tile_index].GetComponent<GridLocation>().grid_data.tile_type 
                != TileType.Road) continue;
                
            
            neighbours.Add(current);
        }

        return neighbours;
    }
    */

    public List<Direction> check_cardinal(GameObject obj, TileType checking_for, int plane)
    {
        // Check the cardinal directions of a GameObject for a specific tile
        // Will be used to check if buildings are connected to roads, ect
        Vector2 start = new Vector2(0, 0);

        if (obj.GetComponent<GridLocation>())
        {
            start = obj.GetComponent<GridLocation>().grid_data.position;
        }
        else if (obj.GetComponent<SOUL>())
        {
            start = obj.GetComponent<SOUL>().position;
        }

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

    public void setConnected(GameObject road, int plane)
    {
        // Call when a road is placed
        // set the connection status of tiles affected

        if (road.GetComponent<GridLocation>())
        {
            Vector2 start = road.GetComponent<GridLocation>().grid_data.position;

            Vector2[] directions = new Vector2[24]
            {
                new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0), // right
                new Vector2(0, -1), new Vector2(0, -2), new Vector2(0, -3), // back
                new Vector2(-1, 0), new Vector2(-2, 0), new Vector2(-3, 0), // left
                new Vector2(0, 1), new Vector2(0, 2), new Vector2(0, 3), // front
                new Vector2(1, 1), new Vector2(1, 2), new Vector2(2, 1), // top right
                new Vector2(1, -1), new Vector2(1, -2), new Vector2(2, -1), // bottom right
                new Vector2(-1, 1), new Vector2(-1, 2), new Vector2(-2, 1), // top left
                new Vector2(-1, -1), new Vector2(-1, -2), new Vector2(-2, -1) // bottom left
            };

            foreach (Vector2 dir in directions)
            {
                Vector2 current = new Vector2(start.x + dir.x, start.y + dir.y);

                if (!withinRange(current)) continue;

                planes[plane][getIndex(current)].GetComponent<GridLocation>().grid_data.connected = true;
            }
        }
    }

    public int soulWalkingDistance(GameObject gate, GameObject zone_building, int plane)
    {
        // find how far SOULs need to walk in order to get from the Gate
        // to their sin/reward
        return 0;
    }

    public int vibe_check(int index, int plane)
    {
        // Get the current vibe of a tile
        return planes[plane][index].GetComponent<GridLocation>().grid_data.vibes;
    }

    public int update_vibe(int plane, int index)
    {
        int radius = planes[plane][index].GetComponent<GridLocation>().grid_data.stored_building.GetComponent<Stats>()
            .vibe_radius;

        return 0;
    }
    
    public int NonGenericTilesAmount()
    {
        var amount = 0;
        var planes = grid_location_list;
        foreach (var plane in planes)
        {
            var plane_grid_data = plane.GetComponent<GridLocation>();
            if (plane_grid_data.grid_data.zone_type != ZoneType.None)
            {
                amount += 1;
            }
        }

        amount_of_changed_tiles = amount;
        return amount;
    }
}


