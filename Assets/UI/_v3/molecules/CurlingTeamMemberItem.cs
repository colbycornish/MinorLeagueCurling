using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

namespace CurlingUI.v3 {
    public class CurlingTeamMemberItem : MonoBehaviour
    {
        [Header("Role")]
        public bool isThrower;
        public bool isLeftSweeper;
        public bool isRightSweeper;

        [Header("Data")]
        public CharacterSO characterData;
        public CurlingBroomSO broomData;
        public List<CurlingStoneSO> selectedStones;

        [Header("Game Objects")]
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textPosition;
        public TextMeshProUGUI textAbilityTitle;
        public TextMeshProUGUI textAbilityDescription;

        public Image bkgAvatar2;
        public Image bkgAvatar1;
        public GameObject Button_EditCharacter;
        public GameObject Button_EditEquipment;

        [Header("Display Areas")]
        public GameObject CharacterEquipmentDisplayArea;
        public GameObject CharacterBroomDisplayArea;
        public GameObject CharacterStonesDisplayArea;
        public GameObject CharacterInfoDisplayArea;
        public GameObject CharacterInfoStatsDisplayArea;
        public GameObject CharacterInfoAbilityDisplayArea;

        [Header("Functions")]
        public UnityEvent EditCharacterEvent;
        public UnityEvent EditEquipmentEvent;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            UpdateCharacterDisplays();
        }
        void OnEnable()
        {
            
        }
        void OnDisable(){}

        void UpdateDisplay()
        {
            // This can be used for any dynamic updates needed each frame, but for now we will call specific update functions when data changes.
        }

        /************************************************************************************************************************/

        public void UpdateCharacterData(CharacterSO newCharacterData)
        {
            characterData = newCharacterData;
            UpdateCharacterDisplays();
            // Update the UI display for the character data
        }

        void UpdateCharacterDisplays()
        {
            UpdateCharacterNameDisplay();
            UpdateCharacterPositionDisplay();
            UpdateCharacterAbilityDisplay();
            UpdateCharacterImageDisplay();
            UpdateCharacterStatsDisplay();
        }

        void UpdateCharacterImageDisplay()
        {
            if (characterData != null)
            {
                // Update the character image display based on the character data
                // For example, you could set the sprite of an Image component to characterData.avatarSprite
                bkgAvatar1.sprite = characterData.AvatarImage;
                bkgAvatar2.sprite = characterData.AvatarImage;
                bkgAvatar1.gameObject.SetActive(true); //.sprite = null;
                bkgAvatar2.gameObject.SetActive(true); //.sprite = null;
            }
            else
            {
                // Set to default or empty sprite if no character data
                bkgAvatar1.gameObject.SetActive(false); //.sprite = null;
                bkgAvatar2.gameObject.SetActive(false); //.sprite = null;
            }
        }

        void UpdateCharacterStatsDisplay(){}

        void UpdateCharacterNameDisplay()
        {
            if (characterData != null)
            {
                textName.text = characterData.Name;
            }
            else
            {
                textName.text = "???????????";
            }
                
        }

        void UpdateCharacterPositionDisplay()
        {
            // Update Position Text
            if (textPosition != null)
            {
                if (isThrower == true)
                {
                    textPosition.text = "Thrower";
                }
                else if (isLeftSweeper == true)
                {
                    textPosition.text = "Left Sweeper";
                }
                else if (isRightSweeper == true)
                {
                    textPosition.text = "Right Sweeper";
                }
            }

            // Update Available Equipment Display
            if (isThrower == true)
            {
                CharacterBroomDisplayArea.SetActive(false);
                CharacterStonesDisplayArea.SetActive(true);
            }
            else if (isLeftSweeper == true)
            {
                CharacterBroomDisplayArea.SetActive(true);
                CharacterStonesDisplayArea.SetActive(false);
            }
            else if (isRightSweeper == true)
            {
                CharacterBroomDisplayArea.SetActive(true);
                CharacterStonesDisplayArea.SetActive(false);
            }

            
        }

        void UpdateCharacterAbilityDisplay()
        {
            if (textAbilityTitle != null)
            {
                if (isThrower == true)
                {
                    textAbilityTitle.text = "Thrower Ability";
                }
                else if (isLeftSweeper == true)
                {
                    textAbilityTitle.text = "Left Sweeper Ability";
                }
                else if (isRightSweeper == true)
                {
                    textAbilityTitle.text = "Right Sweeper Ability";
                }
            }

            if (textAbilityDescription != null)
            {
                if (characterData != null)
                {
                    // textAbilityDescription.text = characterData.abilityDescription;
                }
                else
                {
                    textAbilityDescription.text = "????";
                }
            }
                
        }

        /************************************************************************************************************************/

        public void UpdateBroomData(CurlingBroomSO newBroomData)
        {
            broomData = newBroomData;
            UpdateBroomDisplay();
            // Update the UI display for the broom data
        }

        private void UpdateBroomDisplay()
        {
            
        }

        private void ResetBroomDisplay(){}

        /************************************************************************************************************************/

        public void UpdateStonesData(List<CurlingStoneSO> newStonesData)
        {
            selectedStones = newStonesData;
            UpdateStonesDisplay();
        } 
        private void UpdateStonesDisplay(){}

        public void ResetStonesDisplay(){}

        /************************************************************************************************************************/
     
        public void OnClickEditCharacter()
        {
            if (EditCharacterEvent != null)
            {
                EditCharacterEvent.Invoke();
            }
        }

        public void OnClickEditEquipment()
        {
            if (EditEquipmentEvent != null)
            {
                EditEquipmentEvent.Invoke();
            }
        }

        
        
    }
}
