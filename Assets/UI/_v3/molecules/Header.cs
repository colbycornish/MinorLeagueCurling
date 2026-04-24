using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace CurlingUI.v3 {
    public class Header : MonoBehaviour
    {
        // [SerializeField]
        public GameObject _HeaderContent;

        [Header("Title")]
        public string subtitleTop;
        public string title;
        public string subtitleBottom;

        [Header("Button States")]
        public bool hasBackButton;
        public bool hasContinueButton;

        public bool canContinue;
        
        

        
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        // void Start()
        // {
            
        // }

        // // Update is called once per frame
        // void Update()
        // {
            
        // }
    }
}