using System.Collections.Generic;
using UnityEngine;

namespace CurlingUI.v3 {
    public class CharacterSelectionDialogue : MonoBehaviour
    {
        [Header("Role")]
        public bool displayThrowerOptions;
        public bool displayLeftSweeperOptions;
        public bool displayRightSweeperOptions;

        [Header("Data")]
        public List<ScriptableObject> characterData;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }
        void OnEnable(){}
        void OnDisable(){}

        public void OpenForThrower()
        {
            
        }
        public void OpenForLeftSweeper()
        {
            
        }
        public void OpenForRightSweeper()
        {
            
        }
        public void Close()
        {
            
        }

        public void OnSelectCharacter(int characterIndex)
        {
            
        }
        public void OnPressCharacter(int characterIndex)
        {
            
        }
    }
}
