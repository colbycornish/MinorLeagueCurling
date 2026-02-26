using UnityEngine;
using Unity.Properties;

[CreateAssetMenu(fileName = "CurlingGameSO", menuName = "Scriptable Objects/CurlingGameSO")]
public class CurlingGameSO : ScriptableObject
{
    [SerializeField, DontCreateProperty] 
    private string _Id;

    [CreateProperty] 
    public string Id => _Id;

    //
    [SerializeField, DontCreateProperty] 
    private CurlingCourseSO _Course;

    [CreateProperty] 
    public CurlingCourseSO Course => _Course;

    //
    [SerializeField, DontCreateProperty] 
    private CurlingTeamSO _TeamHome;

    [CreateProperty] 
    public CurlingTeamSO TeamHome => _TeamHome;

    //
    [SerializeField, DontCreateProperty] 
    private CurlingTeamSO _TeamAway;

    [CreateProperty] 
    public CurlingTeamSO TeamAway => _TeamAway;
}
