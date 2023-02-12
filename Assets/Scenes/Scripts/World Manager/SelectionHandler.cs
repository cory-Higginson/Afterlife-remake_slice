using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class SelectionHandler : MonoBehaviour
{
    private GameObject[][] tiles;
    
    private bool zoning;
    private ZoneType current_zoning_type = ZoneType.Blue;
    private Vector3 selection_start;
    private Vector3 selection_end;
    private float ray_cast_timer = 0;
    
    public RectTransform selection_box;
    public List<GridLocation> selected_tiles = new List<GridLocation>();
    
    private void Start()
    {
        InputManager.Instance.my_input_actions.AfterLifeActions.LeftMouse.started += startZoning;
        InputManager.Instance.my_input_actions.AfterLifeActions.LeftMouse.canceled += finishZoning;

        tiles = WorldManager.Instance.getPlanes();
    }

    private void Update()
    {
        ray_cast_timer += Time.deltaTime;

        if (ray_cast_timer >= 0.5f)
        {
            updateGridSelection();
        }
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
        if (current_zoning_type != ZoneType.None)
        {
            zoning = true;

            RaycastHit hit = castMouseRayForTiles();
            if (!hit.transform.IsUnityNull())
            {
                selection_start = hit.point;
                selection_box.position = hit.point;
                selection_box.sizeDelta = Vector2.zero;
                selection_box.anchoredPosition3D = selection_start;
            }
        }
    }

    private void finishZoning(InputAction.CallbackContext context)
    {
        zoning = false;
        selection_box.sizeDelta = Vector2.zero;

        foreach (var tile in selected_tiles)
        {
            tile.grid_data.tile_type = TileType.Zone;
            tile.grid_data.zone_type = current_zoning_type;
        }
        // do something to selection.
    }
}
