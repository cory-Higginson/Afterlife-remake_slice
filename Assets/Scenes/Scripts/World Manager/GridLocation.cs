using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLocation : MonoBehaviour
{
    public GridData grid_data;

    public GameObject building;

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
            default:
                tile_mat.color = Color.white;
                break;
        }

        if (grid_data.tile_type != TileType.None)
        {
            timer += Time.deltaTime;
        }

        if (timer >= max_timer && grid_data.stored_building == null)
        {
            Vector3 spawn_point = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
            grid_data.stored_building = Instantiate(building, spawn_point, Quaternion.identity, this.transform);
        }
    }
}


