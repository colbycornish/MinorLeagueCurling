using UnityEngine;
using Unity.Properties;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CurlingTeamSO", menuName = "Scriptable Objects/CurlingTeamSO")]
public class CurlingTeamSO : ScriptableObject
{
    // Basic Info
    [SerializeField, DontCreateProperty] 
    private string _Id;

    [CreateProperty] 
    public string Id => _Id;

    // Team Members
    [SerializeField, DontCreateProperty] 
    private string _Thrower;

    [CreateProperty] 
    public string Thrower => _Thrower;

    //
    [SerializeField, DontCreateProperty] 
    private string _RightSweeper;

    [CreateProperty] 
    public string RightSweeper => _RightSweeper;

    //
    [SerializeField, DontCreateProperty] 
    private string _LeftSweeper;

    [CreateProperty] 
    public string LeftSweeper => _LeftSweeper;

    // Equipment
    [SerializeField, DontCreateProperty] 
    private CurlingBroomSO _LeftSweeperBroom;

    [CreateProperty] 
    public CurlingBroomSO LeftSweeperBroom => _LeftSweeperBroom;

    //
    [SerializeField, DontCreateProperty] 
    private CurlingBroomSO _RightSweeperBroom;

    [CreateProperty] 
    public CurlingBroomSO RightSweeperBroom => _RightSweeperBroom;

    [SerializeField, DontCreateProperty] 
    private CurlingStoneSO _DefaultStone;

    [CreateProperty] 
    public CurlingStoneSO DefaultStone => _DefaultStone;

    [SerializeField, DontCreateProperty] 
    private List<CurlingStoneSO> _ListOfStones;

    [CreateProperty] 
    public List<CurlingStoneSO> ListOfStones => _ListOfStones;

}
