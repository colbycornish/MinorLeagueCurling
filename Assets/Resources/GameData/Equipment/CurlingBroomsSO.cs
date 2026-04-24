using UnityEngine;
using System.Collections.Generic;
using Unity.Properties;


[CreateAssetMenu(fileName = "CurlingBrooms", menuName = "Scriptable Objects/CurlingBrooms")]
public class CurlingBroomsSO : ScriptableObject
{
    [SerializeField, DontCreateProperty]
    private List<CurlingBroomSO> _CurlingBrooms = new List<CurlingBroomSO>();

    [CreateProperty]
    public List<CurlingBroomSO> CurlingBrooms => _CurlingBrooms;

}
