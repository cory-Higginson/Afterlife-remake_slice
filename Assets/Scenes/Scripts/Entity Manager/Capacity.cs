using UnityEngine;
using System.Collections.Generic;


public class Capacity : MonoBehaviour
{
    [SerializeField] private int MaximumCapacity; //UNCHANGEABLE
    public int CurrentCapacity;
    [SerializeField] private List<SOUL> TemporaryCapacity;
    [SerializeField] private List<SOUL> PermanentCapacity;
};