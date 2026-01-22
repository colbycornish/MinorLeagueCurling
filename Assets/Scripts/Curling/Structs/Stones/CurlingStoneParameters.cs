// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;

namespace CurlingStones
{

    [Serializable]
    public class CurlingStoneParameters
    {

        [SerializeField]
        private CurlingStoneMetaData _MetaData;
        public CurlingStoneMetaData MetaData => _MetaData; 

        [SerializeField]
        private CurlingStoneMovement _Movement;
        public CurlingStoneMovement Movement => _Movement; 
        
        [SerializeField]
        private CurlingStoneStatus _Status;
        public CurlingStoneStatus Status => _Status; 

        /* MOVEMENT ***********************************************************************************************************************/

        
    }
}



// /* TALKING ***********************************************************************************************************************/

// [SerializeField]
// private bool _IsTalking = false;
// public ref bool IsTalking => ref _IsTalking;

// [SerializeField]
// private bool _IsShouting = false;
// public ref bool IsShouting => ref _IsShouting;