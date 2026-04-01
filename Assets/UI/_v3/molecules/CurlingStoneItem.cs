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
        public CurlingStone stoneData;
        public CurlingStoneSO stoneDataSO;
        public Action<CurlingStone> _OnSelectStone;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        
        public void UpdateDisplay()
        {
            if (stoneData != null)
            {
                if (textName != null) textName.text = stoneData.name;
                if (textDescription != null) textDescription.text = stoneData.description;
                if (avatarImage != null) avatarImage.sprite = stoneData.stoneDataSO.AvatarImage;
            }
            else
            {
                if (textName != null) textName.text = "No Character";
                if (textDescription != null) textDescription.text = "";
                if (avatarImage != null) avatarImage.sprite = null;
            }
        }

        public void SelectStone() {
            Debug.Log($"[item] Selected Stone: {stoneData.name}");
            _OnSelectStone?.Invoke(stoneData);
        }
    }
}
