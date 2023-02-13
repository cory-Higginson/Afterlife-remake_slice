public class TierBuilding : GenericBuilding
{
    public string TierName = "basic"; // Tier name
    public TierBuilding NextUpgrade; // next upgrade ref
    public int UpgradeCost = 400000; // cost to upgrade
}
