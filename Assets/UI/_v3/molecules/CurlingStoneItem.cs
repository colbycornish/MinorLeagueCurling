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
        public GameObject usedArea; // An area to show if the stone has been used or not (e.g., an overlay or icon)

        [Header("Data")]
        public CurlingStone stoneData;
        public CurlingStoneSO stoneDataSO;
        public Action<CurlingStone> _OnSelectStone;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        
        public void UpdateDisplay()
        {
            UpdateTextDisplays();
            UpdateImageDisplays();
            UpdateStatusDisplays();
        }

        private void UpdateTextDisplays()
        {
            if (stoneData != null)
            {
                if (textName != null) textName.text = stoneData.name;
                if (textDescription != null) textDescription.text = stoneData.description;
            }
            else
            {
                if (textName != null) textName.text = "No Character";
                if (textDescription != null) textDescription.text = "";
                
            }
        }

        private void UpdateImageDisplays()
        {
            if (stoneData != null)
            {
                if (avatarImage != null) avatarImage.sprite = stoneData.stoneDataSO.AvatarImage;
            }
            else
            {
                if (avatarImage != null) avatarImage.sprite = null;
            }
        }

        private void UpdateStatusDisplays()
        {
            bool stoneHasBeenUsed = false;
            if (stoneData != null)
            {
                stoneHasBeenUsed = stoneData.Parameters.Status.IsInPlay || 
                    stoneData.Parameters.Status.IsInScoringZone || 
                    stoneData.Parameters.Status.IsSliding || 
                    stoneData.Parameters.Status.IsThrown;
            }

            if (stoneHasBeenUsed)
            {
                if (usedArea != null) usedArea.SetActive(true);
                gameObject.GetComponent<Button>().interactable = false; // Disable button if stone has been used
            }
            else
            {
                if (usedArea != null) usedArea.SetActive(false);
                gameObject.GetComponent<Button>().interactable = true; // Enable button if stone has not been used
            }
            
            
        } 

        public void SelectStone() {
            Debug.Log($"[item] Selected Stone: {stoneData.name}");
            _OnSelectStone?.Invoke(stoneData);
        }
    }
}
