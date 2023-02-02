

public class GenericBuilding
{
    public int[] Placeable; // list of all placeable layers

    public string Name; // name
    public uint Cost; // cost of building
    public Capacity CapacityInfo; // capacity struct
    public int CurrentCapacity; // total used
    
    public int Vibe; // good bad vibes
    public string Description; // cool info
    public bool Connected; // attached to road?

    public bool Locked;
    public int Tax;
};

public class TierBuilding : GenericBuilding
{
    public string TierName; // Tier name
    public GenericBuilding NextUpgrade; // next upgrade ref
    public int UpgradeCost; // cost to upgrade
}