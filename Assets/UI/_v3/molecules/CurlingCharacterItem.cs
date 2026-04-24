using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CurlingUI.v3 {
    public class CurlingCharacterItem : MonoBehaviour
    {
        [Header("UI Elements")]
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textDescription;
        public Image avatarImage;

        [Header("Data")]
        public CharacterSO characterData;
        public Action<CharacterSO> _OnSelectCharacter;
        // public Action<CurlingCharacter> _OnSelectCharacter;
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
            if (characterData != null)
            {
                if (textName != null) textName.text = characterData.Name;
                if (textDescription != null) textDescription.text = characterData.Description;
                if (avatarImage != null) avatarImage.sprite = characterData.AvatarImage;
            }
            else
            {
                if (textName != null) textName.text = "No Character";
                if (textDescription != null) textDescription.text = "";
                if (avatarImage != null) avatarImage.sprite = null;
            }
        }

        public void SelectCharacter() {
            Debug.Log($"[item] Selected Character: {characterData.Name}");
            _OnSelectCharacter?.Invoke(characterData);
        }
    
    }
}
