using UnityEngine;
using System.Collections;
using TMPro;
using CurlingManagersV3;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CurlingUI.v3 {
    public class ListOfCurlingGameRules : MonoBehaviour
    {
        public CurlingBroomsSO listOfCurlingBrooms;
        public GameObject ContentArea;
        public GameObject Default;

        [Header("Rule Display Objects")]
        public GameObject RuleGameMode;
        public GameObject RuleScoring;
        public GameObject RuleOpponent;
        public GameObject RuleNumberOfRounds;
        public GameObject RuleDifficulty;
        public GameObject RuleThrowClock;

        [Header("Selections")]
        public Action<CurlingBroomSO> _OnUpdateRules;

        public GameObject currentRules;
        public GameObject currentSelectedRule;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        void OnEnable(){
            // PopulateList();
        } 

        void PopulateList(){
            
        }

        public void OnUpdateDisplay(){}

        public void OnChangeGameMode()
        {
            
        }

        public void OnChangeScoring()
        {
            
        }

        public void OnChangeOpponent()
        {
            
        }

        public void OnChangeNumberOfRounds()
        {
            
        }

        public void OnChangeDifficulty()
        {
            
        }

        public void OnChangeThrowClock()
        {
            
        }
    }
}
