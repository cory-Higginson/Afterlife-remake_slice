using UnityEngine;
using System.Collections.Generic;


public class Capacity : MonoBehaviour
{
    [SerializeField] public int MaximumCapacity; //UNCHANGEABLE
    public int CurrentCapacity;
    [SerializeField] public List<SOUL> TemporaryCapacity;
    [SerializeField] public List<SOUL> PermanentCapacity;
};