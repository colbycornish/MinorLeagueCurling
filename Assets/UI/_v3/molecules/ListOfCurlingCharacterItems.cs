using UnityEngine;
using System.Collections;
using TMPro;
using CurlingManagersV3;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CurlingUI.v3 {
    public class ListOfCurlingCharacterItems : UIListController
    {
        public ListOfCharactersSO listOfCharacters;
        public GameObject ContentArea;
        public GameObject DefaultCharacterItem;

        public Action<CharacterSO> _OnSelectCharacter;
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

            foreach (CharacterSO character in listOfCharacters.ListOfCharacters) {
                GameObject newItem = Instantiate(DefaultCharacterItem, ContentArea.transform);
                CurlingCharacterItem characterItemScript = newItem.GetComponent<CurlingCharacterItem>();

                characterItemScript._OnSelectCharacter = OnSelectCharacter;
                    // OnSelect: (string characterId) => { OnSelection(characterId); }
                characterItemScript.characterData = character;
            }
        }

        public void OnSelectCharacter(CharacterSO character)
        {
            Debug.Log($"[list] Selected Character: {character.Name}");
            _OnSelectCharacter?.Invoke(character);
        }
    }
}
