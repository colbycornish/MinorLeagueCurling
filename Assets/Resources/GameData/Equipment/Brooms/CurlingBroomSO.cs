using UnityEngine;
using Unity.Properties;

[CreateAssetMenu(fileName = "CurlingBroomSO", menuName = "Scriptable Objects/CurlingBroomSO")]
public class CurlingBroomSO : ScriptableObject
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
    private string _Description;

    [CreateProperty] 
    public string Description => _Description;

    [SerializeField, DontCreateProperty] 
    private Sprite _Thumbnail;

    [CreateProperty] 
    public Sprite Thumbnail => _Thumbnail;

    
    // "type": "Item",
    // "rarity": "Common",
    // "iconPath": "",
    // "modelPath": ""

}
