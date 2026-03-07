using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CurlingUI.v3 {
    public class CurlingBroomItem : MonoBehaviour
    {
        [Header("UI Elements")]
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textDescription;
        public Image avatarImage;

        [Header("Data")]
        public CurlingBroomSO broomData;
        public Action<CurlingBroomSO> _OnSelectBroom;
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        
        void Start()
        {
            UpdateDisplay();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void UpdateDisplay()
        {
            if (broomData != null)
            {
                if (textName != null) textName.text = broomData.Name;
                if (textDescription != null) textDescription.text = broomData.Description;
                if (avatarImage != null) avatarImage.sprite = broomData.Thumbnail;
            }
            else
            {
                if (textName != null) textName.text = "No Character";
                if (textDescription != null) textDescription.text = "";
                if (avatarImage != null) avatarImage.sprite = null;
            }
        }

        public void SelectBroom() {
            Debug.Log($"[item] Selected Broom: {broomData.Name}");
            _OnSelectBroom?.Invoke(broomData);
        }
    }
}
