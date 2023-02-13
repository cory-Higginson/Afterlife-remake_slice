using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public enum SOULLocation
{
    wandering,
    zoned,
}


public class SoulManager : MonoBehaviour
{
    [SerializeField] private int totalSouls;
    [SerializeField] private GameObject soulPrefab;
    [SerializeField] private List<SOUL> Souls; // all the souls
    [SerializeField] private List<SOUL> ZonedSouls; // souls have 

    [SerializeField] private List<GameObject> wanderingSouls; // souls that are still finding a "home"
    // DEBUG STATS
    //[SerializeField] private int max_souls;
    //[SerializeField] private int max_wandeing;
    //[SerializeField] private int max_



    private float time = 0.0f;
    [SerializeField] private float timepass = 3.0f;

    public void AddSoul(SOULLocation soulLocation, Vector3 pos, Vector2 grid, Quaternion identity)
    {
        if (soulLocation == SOULLocation.wandering)
        {
            wanderingSouls.Add(Instantiate(soulPrefab, pos, Quaternion.identity));
            wanderingSouls[wanderingSouls.Count - 1].GetComponent<SOUL>().position = grid;
            Souls.Add(wanderingSouls[wanderingSouls.Count - 1].GetComponent<SOUL>());
        }
    }


    public void wander()
    {
        foreach (GameObject soul in wanderingSouls)
        {
            List<Direction> connected = WorldManager.Instance.check_cardinal(soul, TileType.Road, 0);

            int randomdir;
            if (connected.Count >= 1)
            {
                randomdir = Random.Range(0, connected.Count);
            }

            else
            {
                continue;
            }

            switch (connected[randomdir])
            {
                case Direction.North:
                {
                    soul.transform.position += Vector3.forward;
                    soul.GetComponent<SOUL>().position.y++;
                    break;
                }
                case Direction.East:
                {
                    soul.transform.position += Vector3.right;
                    soul.GetComponent<SOUL>().position.x++;
                    break;
                }
                case Direction.South:
                {
                    soul.transform.position += Vector3.back;
                    soul.GetComponent<SOUL>().position.y--;
                    break;
                }
                case Direction.West:
                {
                    soul.transform.position += Vector3.left;
                    soul.GetComponent<SOUL>().position.x--;
                    break;
                }
                case Direction.None:
                default:
                {
                    Debug.LogWarning("AAAAAHHH");
                    throw new ArgumentOutOfRangeException();
                }
            }

            connected.Clear();
        }
    }


    void Checkforhousing()
    {
        foreach (GameObject soul in wanderingSouls)
        {
            ZoneType soul_desire = soul.GetComponent<SOUL>().zonetype;

            // is it direct connect?
            var connected = WorldManager.Instance.check_cardinal(soul, TileType.Zone, 0);

            foreach (var VARIABLE in connected)
            {
                Vector2 loc = soul.GetComponent<SOUL>().position;

                switch (VARIABLE)
                {
                    case Direction.North:
                        loc.y++;
                        break;
                    case Direction.South:
                        loc.y--;
                        break;
                    case Direction.East:
                        loc.x++;
                        break;
                    case Direction.West:
                        loc.x--;
                        break;
                }
                print("run");
                if (WorldManager.Instance.withinRange(loc))
                {
                    print("rin");
                    var griddata = WorldManager.Instance.planes[0][WorldManager.Instance.getIndex(loc)]
                        .GetComponent<GridLocation>().grid_data;

//                  if (griddata.zone_type == soul_desire) ;
//                  {
                        print("test");
                        if (griddata.stored_building.GetComponent<Capacity>()
                                .MaximumCapacity != griddata.stored_building.GetComponent<Capacity>().CurrentCapacity) ;
                        {
                            soul.GetComponent<MeshRenderer>().enabled = false;
                            soul.transform.parent = griddata.stored_building.transform;
                            if (soul.GetComponent<SOUL>().Reincarnate)
                            {
                                griddata.stored_building.GetComponent<Capacity>().TemporaryCapacity
                                    .Add(soul.GetComponent<SOUL>());
                                
                                griddata.stored_building.GetComponent<Capacity>().CurrentCapacity++;
                            }
                            else
                            {
                                griddata.stored_building.GetComponent<Capacity>().PermanentCapacity
                                    .Add(soul.GetComponent<SOUL>());
                                
                                griddata.stored_building.GetComponent<Capacity>().CurrentCapacity++;
                            }
                            ZonedSouls.Add(soul.GetComponent<SOUL>());
                            Souls.Add(soul.GetComponent<SOUL>());
                            wanderingSouls.Remove(soul);
                            continue;
                        }
//                  }
                }
            }
        }
    }


    void Update()
    {
        time += Time.deltaTime;
        totalSouls = wanderingSouls.Count + ZonedSouls.Count;
        if (time >= timepass)
        {
            Checkforhousing();
            wander();
            time = 0;
        }




    }
}