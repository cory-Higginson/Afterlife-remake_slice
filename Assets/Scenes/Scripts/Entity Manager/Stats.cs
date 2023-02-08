using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]private int Vibes = 3;           // good bad vibes
    public bool directConnect;
    public bool Connected;      // attached to road?
    public bool Energised;      // is it near a siphon or not
    public bool Locked;         // it's locked
    public int LockCost;        // its tax-charge
    
}