using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace CurlingUI.v3 {
    public class CurlingStoneItem : MonoBehaviour
    {
        [Header("UI Elements")]
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textDescription;
        public Image avatarImage;

        [Header("Data")]
        public CurlingStoneSO stoneData;
        public Action<CurlingStoneSO> _OnSelectStone;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        
        public void UpdateDisplay()
        {
            if (stoneData != null)
            {
                if (textName != null) textName.text = stoneData.Name;
                if (textDescription != null) textDescription.text = stoneData.Description;
                if (avatarImage != null) avatarImage.sprite = stoneData.AvatarImage;
            }
            else
            {
                if (textName != null) textName.text = "No Character";
                if (textDescription != null) textDescription.text = "";
                if (avatarImage != null) avatarImage.sprite = null;
            }
        }

        public void SelectStone() {
            Debug.Log($"[item] Selected Stone: {stoneData.Name}");
            _OnSelectStone?.Invoke(stoneData);
        }
    }
}
