[System.Serializable]

public class CharacterInfo
{
    public string id;
    public string version;
    public string firstName;
    public string lastName;
    public string fullName => $"{firstName} {lastName}";
    public string description;
    public string iconPath; // Path to the icon asset
    public string addressableModelPath; // Path to the model asset
    public CurlingPlayer CurlingPlayerData;
    // public GameObject model;

}