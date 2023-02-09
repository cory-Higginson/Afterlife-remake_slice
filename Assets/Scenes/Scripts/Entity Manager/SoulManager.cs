using UnityEngine;
using System.Collections.Generic;

using WM = WorldManager.Instance;

class SoulManager : MonoBehaviour
{


    private List<GameObject> Souls;           // all the souls
    
    private List<SOUL> ZonedSouls;            // souls have 
    private List<GameObject> wanderingSouls;  // souls that are still finding a "home"

    private float time = 0.0f;
    [SerializeField] private float timepass = 3.0f;



    public void wander()
    {
        foreach (GameObject soul in wanderingSouls)
        {
           var connected = WM.check_cardinal(soul,TileType.Road,0);
        }
    }

    void Update()
    {

        if (time > timepass) { wander(); }



        
    }
}