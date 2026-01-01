// // Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

// #pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

// using System;
// using UnityEngine;
// using Animancer;

// namespace CharacterNPC
// {

//     [Serializable]
//     public class CharacterParameters
//     {
//         /* MOVEMENT ***********************************************************************************************************************/

//         [SerializeField]
//         private Vector3 _MovementDirection;
//         public Vector3 MovementDirection
//         {
//             get => _MovementDirection;
//             set => _MovementDirection = Vector3.ClampMagnitude(value, 1);
//         }

//         public float ForwardSpeed { get; set; }
//         public float DesiredForwardSpeed { get; set; }
//         public float VerticalSpeed { get; set; }

//         [SerializeField]
//         private Transform _CurrentDestination;
//         public Transform CurrentDestination
//         {
//             get => _CurrentDestination;
//             set => _CurrentDestination = value;
//         }

//         [SerializeField]
//         private float _DistanceFromDestination = 0f;
//         public ref float DistanceFromDestination => ref _DistanceFromDestination;

//         [SerializeField]
//         private bool _WantsToRun;
//         public ref bool WantsToRun => ref _WantsToRun;


//         [SerializeField]
//         private bool _IsPatrolling;
//         public ref bool IsPatrolling => ref _IsPatrolling;

//         [SerializeField]
//         private bool _IsWandering;
//         public ref bool IsWandering => ref _IsWandering;

//         /* ORIENTATION ***********************************************************************************************************************/
//         // [SerializeField]
//         // private bool _IsStanding;
//         // public ref bool IsStanding => ref _IsStanding;


//         [SerializeField]
//         private bool _IsCrouching = false;
//         public ref bool IsCrouching => ref _IsCrouching;

//         [SerializeField]
//         private bool _IsSitting = false;
//         public ref bool IsSitting => ref _IsSitting;

//         [SerializeField]
//         private bool _IsLayingDown = false;
//         public ref bool IsLayingDown => ref _IsLayingDown;

//         /* HUNGER ***********************************************************************************************************************/

//         [SerializeField]
//         private float _HungerLevel = 0.8f;
//         public ref float HungerLevel => ref _HungerLevel;

//         /* SLEEPING ***********************************************************************************************************************/

//         [SerializeField]
//         private float _SleepinessLevel = 0f;
//         public ref float SleepinessLevel => ref _SleepinessLevel;

//         /************************************************************************************************************************/

//         [SerializeField]
//         private bool _IsDead = false;
//         public ref bool IsDead => ref _IsDead;

//         /* TALKING ***********************************************************************************************************************/

//         [SerializeField]
//         private bool _IsTalking = false;
//         public ref bool IsTalking => ref _IsTalking;

//         [SerializeField]
//         private bool _IsShouting = false;
//         public ref bool IsShouting => ref _IsShouting;

//         /* POSE ***********************************************************************************************************************/

//         [SerializeField]
//         private bool _IsHoldingPose = false;
//         public ref bool IsHoldingPose => ref _IsHoldingPose;

        
//     }
// }
