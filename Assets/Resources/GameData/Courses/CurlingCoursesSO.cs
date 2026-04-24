using UnityEngine;
using System.Collections.Generic;
using Unity.Properties;


[CreateAssetMenu(fileName = "CurlingCourses", menuName = "Scriptable Objects/CurlingCourses")]
public class CurlingCoursesSO : ScriptableObject
{
    [SerializeField, DontCreateProperty]
    private List<CurlingCourseSO> _CurlingCourses = new List<CurlingCourseSO>();

    [CreateProperty]
    public List<CurlingCourseSO> CurlingCourses => _CurlingCourses;

}
