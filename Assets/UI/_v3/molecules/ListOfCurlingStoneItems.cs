using UnityEngine;
using System.Collections;
using TMPro;
using CurlingManagersV3;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CurlingUI.v3 {
    public class ListOfCurlingStoneItems : UIListController
    {
        public CurlingStonesSO listOfCurlingStones;
        public GameObject ContentArea;
        public GameObject DefaultCurlingStoneItem;

        public Action<CurlingStoneSO> _OnSelectStone;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // void OnEnable(){
        //     PopulateList();
        // } 

        // void PopulateList(){
        //     foreach (Transform child in ContentArea.transform) {
        //         Destroy(child.gameObject);
        //     }

        //     foreach (CurlingStoneSO stone in listOfCurlingStones.CurlingStones) {
        //         GameObject newItem = Instantiate(DefaultCurlingStoneItem, ContentArea.transform);
        //         CurlingStoneItem stoneItemScript = newItem.GetComponent<CurlingStoneItem>();

        //         stoneItemScript._OnSelectStone = OnSelectStone;
        //             // OnSelect: (string characterId) => { OnSelection(characterId); }
        //         stoneItemScript.stoneData = stone;
        //     }
        // }

        // public void OnSelectStone(CurlingStoneSO stone)
        // {
        //     Debug.Log($"[list] Selected Stone: {stone.Name}");
        //     _OnSelectStone?.Invoke(stone);
        // }
    }
}
