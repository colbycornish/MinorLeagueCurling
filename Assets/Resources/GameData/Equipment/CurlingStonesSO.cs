using UnityEngine;
using System.Collections.Generic;
using Unity.Properties;


[CreateAssetMenu(fileName = "CurlingStones", menuName = "Scriptable Objects/CurlingStones")]
public class CurlingStonesSO : ScriptableObject
{
    [SerializeField, DontCreateProperty]
    private List<CurlingStoneSO> _CurlingStones = new List<CurlingStoneSO>();

    [CreateProperty]
    public List<CurlingStoneSO> CurlingStones => _CurlingStones;

}
