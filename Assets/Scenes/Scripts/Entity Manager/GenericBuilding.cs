//using System;

using System;
using UnityEngine;


//////////////////////////////////////////////////////////////////////////////////////////////////////

public class GenericBuilding : MonoBehaviour
{
    public int[] placeable;         // list of all placeable layers

    [SerializeField]private string buildName = "Generic name";             // name
    public uint efficiency = 100;         // Efficiency rating
    public uint cost = 30;               // cost of building
    private Capacity capacityInfo;  // capacity struct
    
    
    public string description = "this building is decades old";      // cool info

    private Stats stats;            //Stats

    private void Awake()
    {
        stats = gameObject.AddComponent<Stats>();
        capacityInfo = gameObject.AddComponent<Capacity>();
        
    }
};