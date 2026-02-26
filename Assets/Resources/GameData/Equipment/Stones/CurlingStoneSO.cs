using UnityEngine;
using Unity.Properties;

public class CurlingStoneSO : MonoBehaviour
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
    private Texture _Thumbnail;

    [CreateProperty] 
    public Texture Thumbnail => _Thumbnail;
}
