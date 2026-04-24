using UnityEngine;
using System.Collections;
using TMPro;
using CurlingManagersV3;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CurlingUI.v3 {
    public class ListOfCurlingBroomItems : UIListController
    {
        public CurlingBroomsSO listOfCurlingBrooms;
        public GameObject ContentArea;
        public GameObject DefaultCurlingBroomItem;

        public Action<CurlingBroomSO> _OnSelectBroom;
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

            foreach (CurlingBroomSO broom in listOfCurlingBrooms.CurlingBrooms) {
                GameObject newItem = Instantiate(DefaultCurlingBroomItem, ContentArea.transform);
                CurlingBroomItem broomItemScript = newItem.GetComponent<CurlingBroomItem>();

                broomItemScript._OnSelectBroom = OnSelectBroom;
                    // OnSelect: (string characterId) => { OnSelection(characterId); }
                broomItemScript.broomData = broom;
            }
        }

        public void OnSelectBroom(CurlingBroomSO broom)
        {
            Debug.Log($"[list] Selected Broom: {broom.Name}");
            _OnSelectBroom?.Invoke(broom);
        }
    }
}
