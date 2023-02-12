using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

enum SOULLocation
{
    wandering,
    zoned,
}


class SoulManager : MonoBehaviour
{
    [SerializeField] private int totalSouls;
    [SerializeField] private GameObject soulPrefab;
    [SerializeField] private List<SOUL> Souls;           // all the souls
    [SerializeField] private List<SOUL> ZonedSouls;            // souls have 
    [SerializeField] private List<GameObject> wanderingSouls;  // souls that are still finding a "home"

    // DEBUG STATS
    //[SerializeField] private int max_souls;
    //[SerializeField] private int max_wandeing;
    //[SerializeField] private int max_
    


    private float time = 0.0f;
    [SerializeField] private float timepass = 3.0f;

    public void AddSoul(SOULLocation soulLocation, Vector3 pos,Vector2 grid, Quaternion identity)
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
            foreach (var VARIABLE in connected)
            {
                print(VARIABLE);
            }

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

    void Update()
    {
        time += Time.deltaTime;
        totalSouls = wanderingSouls.Count + ZonedSouls.Count;
        if (time >= timepass) 
        { 
            wander();
            time = 0;
        }



        
    }
}