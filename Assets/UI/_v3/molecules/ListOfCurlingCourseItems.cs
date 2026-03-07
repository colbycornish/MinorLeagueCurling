using UnityEngine;
using System.Collections;
using TMPro;
using CurlingManagersV3;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using MoreMountains.Feedbacks;

namespace CurlingUI.v3 {
    public class ListOfCurlingCourseItems : UIListController
    {
        public CurlingCoursesSO listOfCurlingCourses;
        public GameObject ContentArea;
        public GameObject DefaultCurlingCourseItem;
        public MMF_Player feedbackOnSelect;

        public Action<CurlingCourseSO> _OnSelectCourse;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        void OnEnable(){
            PopulateList();
        } 

        void PopulateList(){
            foreach (Transform child in ContentArea.transform) {
                Destroy(child.gameObject);
            }

            foreach (CurlingCourseSO course in listOfCurlingCourses.CurlingCourses) {
                GameObject newItem = Instantiate(DefaultCurlingCourseItem, ContentArea.transform);
                CurlingCourseItem courseItemScript = newItem.GetComponent<CurlingCourseItem>();

                courseItemScript._OnSelectCourse = OnSelectCourse;
                    // OnSelect: (string characterId) => { OnSelection(characterId); }
                courseItemScript.courseData = course;
            }
        }

        public void OnSelectCourse(CurlingCourseSO course)
        {
            Debug.Log($"[list] Selected Course: {course.name}");
            _OnSelectCourse?.Invoke(course);
            if (feedbackOnSelect != null)
            {
                feedbackOnSelect.PlayFeedbacks();
            }
        }
    }
}
