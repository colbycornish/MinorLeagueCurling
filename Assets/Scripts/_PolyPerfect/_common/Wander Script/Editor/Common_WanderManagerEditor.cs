using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace Polyperfect.Common
{
    [CustomEditor(typeof(Common_WanderManager))]
    public class Common_WanderManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
  
            GUILayout.EndHorizontal();

            Common_WanderManager Manager = (Common_WanderManager)target;

            if (!Application.isPlaying)
            {
                base.OnInspectorGUI();
                return;
            }


        }
    }
}
#endif