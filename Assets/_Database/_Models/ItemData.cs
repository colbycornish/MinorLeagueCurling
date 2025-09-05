[System.Serializable]
public class ItemData
{
    public string id;
    public string version;
    public string name;
    public string description;
    public string type; // e.g., "Item", "Weapon", "Armor"
    public string rarity; // e.g., "Common", "Uncommon", "Rare"
    public string iconPath; // Path to the icon asset
    public string modelPath; // Path to the model asset
}