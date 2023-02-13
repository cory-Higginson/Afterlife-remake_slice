using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]public int vibes = 3;           // good bad vibes
    [SerializeField]public int vibe_radius;
    public bool directConnect;
    public bool Connected;      // attached to road?
    public bool Energised;      // is it near a siphon or not
    public bool Locked;         // it's locked
    public int LockCost;        // its tax-charge
    public int size = 1;
    
}