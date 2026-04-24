using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Playables;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace PlayerControls.v3
{
    public class PlayerInputTest : MonoBehaviour
    {
        [Header("Actions")]
        private InputAction skipCinematicsAction;

        void Awake()
        {
            Debug.Log("StartGameIntroState Awake: Finding SkipCinematicsAction");
            skipCinematicsAction = InputSystem.actions.FindAction("Jump", true);
        }

        protected virtual void OnEnable()
        {
            skipCinematicsAction.performed += OnSkipCinematicsAction;
            skipCinematicsAction.Enable();
        }

        void Update()
        {

            // if (Keyboard.current.anyKey.wasPressedThisFrame)
            // {
            //     Debug.Log("Space key was pressed");
            //     Test();
            // }
            
        }

        void Test()
        {
            // InputSystem.
            foreach (var action in InputSystem.actions)
            {
                
                if (action.triggered)
                {
                    Debug.Log($"Action triggered: {action.name}");
                }
            };   
        }

        

        private void OnSkipCinematicsAction(InputAction.CallbackContext obj)
        {
            Debug.Log("Skip Cinematics Action Performed");
            // MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.RoundSplash);
        }


    }
}

