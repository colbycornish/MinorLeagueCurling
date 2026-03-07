using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CurlingUI.v3 {
    public class CurlingCourseItem : MonoBehaviour
    {
        [Header("UI Elements")]
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textDescription;
        public Image imageThumbnail;

        [Header("Data")]
        public CurlingCourseSO courseData;
        public Action<CurlingCourseSO> _OnSelectCourse;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            BuildDisplay();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void BuildDisplay() {
            if (courseData != null)
            {
                if (textName != null) textName.text = courseData.Title;
                if (textDescription != null) textDescription.text = courseData.Description;
                if (imageThumbnail != null) imageThumbnail.sprite = courseData.Thumbnail;
            }
            else
            {
                if (textName != null) textName.text = "No Course";
                if (textDescription != null) textDescription.text = "";
                if (imageThumbnail != null) imageThumbnail.sprite = null;
            }
        }

        public void SelectCourse() {
            Debug.Log($"[item] Selected Course: {courseData.Title}");
            _OnSelectCourse?.Invoke(courseData);
        }
    }
}
