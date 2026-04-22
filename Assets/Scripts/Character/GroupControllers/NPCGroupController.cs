using UnityEngine;
using CharacterNPCJobs;
using System.Collections.Generic;
using CharacterNPC.v2;

namespace NPCGroupController
{
    public abstract class NPCGroupController : MonoBehaviour
    {
        public List<CharacterNPC.v2.Character> _listOfCharacters;
        public List<Transform> _patrolLocations;

        #if UNITY_EDITOR
        protected void OnValidate()
        {
            CharacterNPC.v2.Character[] listedStates = GetComponentsInChildren<CharacterNPC.v2.Character>();
            _listOfCharacters = new List<CharacterNPC.v2.Character>(listedStates);
        }
        #endif

        





    }
}
