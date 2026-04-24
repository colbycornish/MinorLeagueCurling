using UnityEngine;
using Unity.Properties;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    [SerializeField, DontCreateProperty] 
    private string _Id;

    [CreateProperty] 
    public string Id => _Id;

    [SerializeField, DontCreateProperty] 
    private string _Version;

    [CreateProperty] 
    public string Version => _Version;

    [SerializeField, DontCreateProperty] 
    private string _Name;

    [CreateProperty] 
    public string Name => _Name;

    [SerializeField, DontCreateProperty] 
    private string _FirstName;

    [CreateProperty] 
    public string FirstName => _FirstName;

    [SerializeField, DontCreateProperty] 
    private string _LastName;

    [CreateProperty] 
    public string LastName => _LastName;

    [SerializeField, DontCreateProperty] 
    private string _Description;

    [CreateProperty] 
    public string Description => _Description;

    [SerializeField, DontCreateProperty] 
    private Sprite _AvatarImage;

    [CreateProperty] 
    public Sprite AvatarImage => _AvatarImage;

    [SerializeField, DontCreateProperty] 
    private GameObject _GameModel;

    [CreateProperty] 
    public GameObject GameModel => _GameModel;

}
