using UnityEngine;
using System.Collections.Generic;
using Unity.Properties;

[CreateAssetMenu(fileName = "CurlingCourseSO", menuName = "Scriptable Objects/CurlingCourseSO")]
public class CurlingCourseSO : ScriptableObject
{
    [SerializeField, DontCreateProperty] 
    private string _Title;
    
    [CreateProperty] 
    public string Title => _Title;

    [SerializeField, DontCreateProperty] 
    private Texture _Thumbnail;

    [CreateProperty] 
    public Texture Thumbnail => _Thumbnail;
}
