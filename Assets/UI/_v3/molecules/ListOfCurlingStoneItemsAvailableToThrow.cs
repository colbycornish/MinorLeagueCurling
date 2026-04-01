using UnityEngine;
using System.Collections;
using TMPro;
using CurlingManagersV3;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CurlingUI.v3 {
    public class ListOfCurlingStoneItemsAvailableToThrow : MonoBehaviour//UIListController
    {
        public CurlingStonesSO listOfCurlingStones;
        public List<CurlingStone> listOfAvailableStones;
        public GameObject ContentArea;
        public GameObject DefaultCurlingStoneItem;

        public Action<CurlingStone> _OnSelectStone;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        void OnEnable(){
        } 

        

        public void BuildList()
        {
            foreach (CurlingStone stone in listOfAvailableStones) {
                GameObject newItem = Instantiate(DefaultCurlingStoneItem, ContentArea.transform);
                CurlingStoneItem stoneItemScript = newItem.GetComponent<CurlingStoneItem>();

                stoneItemScript._OnSelectStone = OnSelectStone;
                    // OnSelect: (string characterId) => { OnSelection(characterId); }
                // stoneItemScript.stoneDataSO = stone;
                stoneItemScript.stoneData = stone;
                stoneItemScript.UpdateDisplay();
            }
        }

        public void Reset()
        {
            foreach (Transform child in ContentArea.transform) {
                Destroy(child.gameObject);
            }
        }

        public void OnSelectStone(CurlingStone stone)
        {
            Debug.Log($"[list] Selected Stone: {stone.name}");
            _OnSelectStone?.Invoke(stone);
        }
    }
}
