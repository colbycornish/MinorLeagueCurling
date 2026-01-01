// // Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

// #pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

// using Animancer.Units;
// using UnityEngine;
// using Animancer;

// namespace CharacterNPC
// {
//     /// <summary>
//     /// Demonstrates how to use layers to play multiple
//     /// independent animations at the same time on different body parts.
//     /// </summary>
//     /// 
//     /// <remarks>
//     /// <strong>Sample:</strong>
//     /// <see href="https://kybernetik.com.au/animancer/docs/samples/layers/dynamic">
//     /// Dynamic Layers</see>
//     /// </remarks>
//     /// 
//     /// https://kybernetik.com.au/animancer/api/Animancer.Samples.Layers/DynamicLayeredCharacterAnimations
//     /// 
//     [AddComponentMenu(Strings.SamplesMenuPrefix + "Layers - Dynamic Layered Character Animations")]
    
//     public class DynamicLayeredCharacterAnimations : MonoBehaviour
//     {
//         /************************************************************************************************************************/

//         [SerializeField] private LayeredAnimationManager _AnimationManager;
//         [SerializeField] private ClipTransition _Idle;
//         [SerializeField] private ClipTransition _IdleCrouch;
//         [SerializeField] private ClipTransition _Move;
//         [SerializeField] private ClipTransition _MoveCrouch;
//         [SerializeField] private ClipTransition _Action;
//         // [SerializeField] private ClipTransition _Crouch;
//         private bool throwTriggered = false;

//         /************************************************************************************************************************/

//         protected virtual void Awake()
//         {
//             _Action.Events.OnEnd = _AnimationManager.FadeOutAction;
//         }

//         /************************************************************************************************************************/

//         protected virtual void Update()
//         {
//             UpdateMovement();
//             UpdateAction();
//         }

//         /************************************************************************************************************************/

//         private void UpdateMovement()
//         {
//             float forward = 1; //SampleInput.WASD.y;
//             // float down = SampleInput.WASD.x;
//             if (forward > 0)
//             {
//                 if (throwTriggered == true)
//                 {   
//                     // public void PlayBase(ITransition transition, bool canPlayActionFullBody)
//                     _AnimationManager.PlayBase(
//                         transition: _MoveCrouch, 
//                         canPlayActionFullBody: false
//                     );
//                 }
//                 else
//                 {
//                     _AnimationManager.PlayBase(
//                         transition: _Move, 
//                         canPlayActionFullBody: false
//                     );
//                 }
                
//             }
//             else
//             {
//                 if (throwTriggered == true)
//                 {
//                     _AnimationManager.PlayBase(
//                         transition: _IdleCrouch, 
//                         canPlayActionFullBody: false
//                     );
//                 } else
//                 {
//                     _AnimationManager.PlayBase(
//                         transition: _Idle, 
//                         canPlayActionFullBody: false
//                     );
//                 }
                
                
//             }
//         }

//         /************************************************************************************************************************/

//         private void UpdateAction()
//         {
//             // throwTriggered == false
//             // if (SampleInput.LeftMouseUp && throwTriggered == false)
//             if (throwTriggered == false && Input.GetKeyDown(KeyCode.Space))
//             {
//                 throwTriggered = true;
//                 _AnimationManager.PlayAction(
//                     transition: _Action
//                 );
//             }
//             if (throwTriggered == true)
//             {
//                 throwTriggered = false;
//             }
//         }

//         /************************************************************************************************************************/
//     }
// }
