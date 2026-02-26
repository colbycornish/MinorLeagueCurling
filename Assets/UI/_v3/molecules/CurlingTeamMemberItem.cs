using UnityEngine;
using TMPro;
using Animancer;

namespace CurlingUI.v3 {
    public class CurlingTeamMemberItem : MonoBehaviour
    {
        [Header("Role")]
        public bool isThrower;
        public bool isLeftSweeper;
        public bool isRightSweeper;

        [Header("Data")]
        public ScriptableObject characterData;
        public ScriptableObject broomData;
        public ScriptableObject selectedStones;

        [Header("Game Objects")]
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textPosition;
        public GameObject Button_EditCharacter;
        public GameObject Button_EditEquipment;

        private GameObject CharacterEquipmentDisplayArea;
        public GameObject CharacterBroomDisplayArea;
        public GameObject CharacterStonesDisplayArea;
        private GameObject CharacterInfoDisplayArea;
        private GameObject CharacterInfoStatsDisplayArea;
        private GameObject CharacterInfoAbilityDisplayArea;

        [Header("Functions")]
        public UnityEvent EditCharacterEvent;
        public UnityEvent EditEquipmentEvent;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            UpdateCharacterData();
        }
        void OnEnable()
        {
            UpdateCharacterData();
        }
        void OnDisable(){}

        void UpdateCharacterData()
        {
            UpdateCharacterPositionDisplay();
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



        void UpdateBroomData(){}
        void UpdateSelectedStones(){}

     
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
