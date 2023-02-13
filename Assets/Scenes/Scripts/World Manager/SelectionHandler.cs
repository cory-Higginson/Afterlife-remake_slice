using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.Animations;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SelectionHandler : Singleton<SelectionHandler>
{
    private GameObject[][] tiles;
    
    private bool zoning;
    public ZoneType current_zoning_type = ZoneType.None;
    public bool placing_transport_paths = true;
    private Vector3 selection_start;
    private Vector3 selection_end;
    private GridLocation start_tile;
    private GridLocation end_tile;
    private float ray_cast_timer = 0;
    private Vector2 transport_start_end_delta;
    
    public RectTransform selection_box;
    public List<GridLocation> selected_tiles = new List<GridLocation>();
    
    // Economy
    [SerializeField] private EconomyManager _economyManager;

    private void Start()
    {
        // Find Economy manager
        _economyManager = FindObjectOfType<EconomyManager>();
        
        InputManager.Instance.my_input_actions.AfterLifeActions.LeftMouse.started += startZoning;
        InputManager.Instance.my_input_actions.AfterLifeActions.LeftMouse.canceled += finishZoning;
        InputManager.Instance.my_input_actions.AfterLifeActions.BlueZoning.started += changeZoning;
        InputManager.Instance.my_input_actions.AfterLifeActions.BrownZoning.started += changeZoning;
        InputManager.Instance.my_input_actions.AfterLifeActions.GenericZoning.started += changeZoning;
        InputManager.Instance.my_input_actions.AfterLifeActions.GreenZoning.started += changeZoning;
        InputManager.Instance.my_input_actions.AfterLifeActions.OrangeZoning.started += changeZoning;
        InputManager.Instance.my_input_actions.AfterLifeActions.PurpleZoning.started += changeZoning;
        InputManager.Instance.my_input_actions.AfterLifeActions.RedZoning.started += changeZoning;
        InputManager.Instance.my_input_actions.AfterLifeActions.YellowZoning.started += changeZoning;

        tiles = WorldManager.Instance.getPlanes();
    }

    private void Update()
    {
        ray_cast_timer += Time.deltaTime;

        if (ray_cast_timer >= 0.05f)
        {
            ray_cast_timer = 0;
            
            if (!placing_transport_paths)
            {
                updateGridSelection();
            }
            else
            {
                updateTransportPathSelection();
            }
        }
    }

    private void updateTransportPathSelection()
    {
        if (zoning)
        {
            RaycastHit hit = castMouseRayForTiles();

            if (!hit.transform.IsUnityNull())
            {
                end_tile = hit.transform.gameObject.GetComponent<GridLocation>();
                updateSelectedPath();
            }
        }
    }

    private void updateSelectedPath()
    {
        /*
        transport_start_end_delta = new Vector2(end_tile.grid_data.position.x - start_tile.grid_data.position.x,
            end_tile.grid_data.position.y - start_tile.grid_data.position.y);
        
        int big = Convert.ToInt32(Math.Abs(transport_start_end_delta.x) > Math.Abs(transport_start_end_delta.y)
            ? transport_start_end_delta.x
            : transport_start_end_delta.y);

        int small = Convert.ToInt32(big != Convert.ToInt32(transport_start_end_delta.x)
            ? transport_start_end_delta.x
            : transport_start_end_delta.y);

        while (big != small)
        {
            int step = (big < 0 ? -1 : 1);
            
            
        }
        */


    }

    private void updateGridSelection()
    {
        if (zoning)
        {
            RaycastHit hit = castMouseRayForTiles();

            if (!hit.transform.IsUnityNull())
            {
                selection_end = hit.point;
                calculateSelectionBoxSize();
                checkTilesWithinSelection();
            }
        }
        else
        {
            RaycastHit hit = castMouseRayForTiles();

            if (!hit.transform.IsUnityNull())
            {
                GameObject hit_obj = hit.transform.gameObject;
                
                if (selected_tiles.Count == 0)
                {
                    selected_tiles.Add(hit_obj.GetComponent<GridLocation>());
                    hit_obj.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (hit_obj != selected_tiles[0].gameObject)
                {
                    deselectTiles();

                    selected_tiles.Add(hit_obj.GetComponent<GridLocation>());
                    hit_obj.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            else
            {
                if (selected_tiles.Count != 0)
                {
                    deselectTiles();
                }
            }
        }
    }
    
    private void checkTilesWithinSelection()
    {
        Bounds bounds = new Bounds(selection_box.anchoredPosition3D, new Vector3(selection_box.sizeDelta.x, 1, selection_box.sizeDelta.y));
        deselectTiles();

        for (int plane = 0; plane < tiles.Length; plane++)
        {
            for (int tile = 0; tile < tiles[plane].Length; tile++)
            {
                if (bounds.Intersects(tiles[plane][tile].GetComponent<BoxCollider>().bounds))
                { 
                    selected_tiles.Add(tiles[plane][tile].GetComponent<GridLocation>());
                    tiles[plane][tile].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }
    
    private void calculateSelectionBoxSize()
    {
        float width = selection_end.x - selection_start.x;
        float height = selection_end.y - selection_start.y;
        float length = selection_end.z - selection_start.z;

        selection_box.anchoredPosition3D = selection_start + new Vector3(width/2, height/2, length/2);
        selection_box.sizeDelta = new Vector2(Math.Abs(width), Math.Abs(length));
    }
    
    private RaycastHit castMouseRayForTiles()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), 
                Camera.main.gameObject.transform.forward, out hit, 500, 
                1 << LayerMask.NameToLayer("GridTiles")))
        {
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.gameObject.transform.forward * hit.distance, Color.magenta);
        }
        
        return hit;
    }
    
    private void deselectTiles()
    {
        foreach (var tile in selected_tiles)
        {
            tile.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        
        selected_tiles.Clear();
    }

    private void startZoning(InputAction.CallbackContext context)
    {
        RaycastHit hit = castMouseRayForTiles();
        
        if (!placing_transport_paths && current_zoning_type != ZoneType.None)
        {
            zoning = true;
            if (!hit.transform.IsUnityNull())
            {
                selection_start = hit.point;
                selection_box.position = hit.point;
                selection_box.sizeDelta = Vector2.zero;
                selection_box.anchoredPosition3D = selection_start;
            }
        }
        else if (placing_transport_paths)
        {
            zoning = true;
            if (!hit.transform.IsUnityNull())
            {
                selected_tiles.Clear();
                start_tile = hit.transform.gameObject.GetComponent<GridLocation>();
                selected_tiles.Add(start_tile);
            }
        }
    }

    private void finishZoning(InputAction.CallbackContext context)
    {
        zoning = false;
        selection_box.sizeDelta = Vector2.zero;

        if (!placing_transport_paths)
        {
            foreach (var tile in selected_tiles)
            {
                tile.grid_data.tile_type = TileType.Zone;
                tile.grid_data.zone_type = current_zoning_type;
                _economyManager.RemovePennies(_economyManager.current_cost);
            }
        }
        else
        {
            foreach (var tile in selected_tiles)
            {
                tile.grid_data.tile_type = TileType.Road;
                WorldManager.Instance.setConnected(tile.gameObject, tile.grid_data.plane);
                _economyManager.RemovePennies(_economyManager.current_cost);
            }
        }
    }

    public void changeZoning(InputAction.CallbackContext context)
    {
        if (context.action.name == InputManager.Instance.my_input_actions.AfterLifeActions.BlueZoning.name)
        {
            changeZoneType(ZoneType.Blue);
        }
        else if (context.action.name == InputManager.Instance.my_input_actions.AfterLifeActions.BrownZoning.name)
        {
            changeZoneType(ZoneType.Brown);
        }
        else if (context.action.name == InputManager.Instance.my_input_actions.AfterLifeActions.GenericZoning.name)
        {
            changeZoneType(ZoneType.Generic);
        }
        else if (context.action.name == InputManager.Instance.my_input_actions.AfterLifeActions.GreenZoning.name)
        {
            changeZoneType(ZoneType.Green);
        }
        else if (context.action.name == InputManager.Instance.my_input_actions.AfterLifeActions.OrangeZoning.name)
        {
            changeZoneType(ZoneType.Orange);
        }
        else if (context.action.name == InputManager.Instance.my_input_actions.AfterLifeActions.PurpleZoning.name)
        {
            changeZoneType(ZoneType.Purple);
        }
        else if (context.action.name == InputManager.Instance.my_input_actions.AfterLifeActions.RedZoning.name)
        {
            changeZoneType(ZoneType.Red);
        }
        else if (context.action.name == InputManager.Instance.my_input_actions.AfterLifeActions.YellowZoning.name)
        {
            changeZoneType(ZoneType.Yellow);
        }
    }

    public void changeZoneType(ZoneType zone)
    {
        current_zoning_type = zone;
        uint cost = 1000; 
        // Update the cost of zone accordingly to the current ZoneType
        if (zone != ZoneType.Generic)
        {
            cost = 2500;
        }
        
        
        _economyManager.updateCost(cost);

        //_economyManager.current_cost = cost; //(zone == ZoneType.None ? 1000 : 2500);
    }
}
