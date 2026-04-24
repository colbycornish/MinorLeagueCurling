using System;
using UnityEngine;
using TMPro;

namespace CurlingUI.v3 {
    public class CourseItemV3 : MonoBehaviour
    {
        [Header("UI Elements")]
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textDescription;

        [Header("Data")]
        public CurlingCourseSO courseData;
        public Action<CurlingCourseSO> _OnSelectCourse;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void SelectCourse() {
            Debug.Log($"[item] Selected Course: {courseData.Title}");
            _OnSelectCourse?.Invoke(courseData);
        }
    }
}
