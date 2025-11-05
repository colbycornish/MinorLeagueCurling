// using UnityEngine;
// using System;

// /// <summary>
// /// The central resource for updating and referencing the current phase of a curling match.
// /// </summary>

// public class CurlingMatchPhaseManager : MonoBehaviour
// {
//     public static CurlingMatchPhaseManager Instance { get; private set; }
//     public CurlingMatchPhase CurrentPhase { get; private set; }
//     public event Action<CurlingMatchPhase> OnPhaseChanged;

//     private void Awake()
//     {
//         if (Instance != null)
//         {
//             Destroy(gameObject);
//             return;
//         }
//         Instance = this;
//     }

//     public void SetPhase(CurlingMatchPhase newPhase)
//     {
//         Debug.Log($"[MatchPhase] {CurrentPhase} → {newPhase}");
//         if (CurrentPhase == newPhase) return;

//         CurrentPhase = newPhase;
//         Debug.Log($"[MatchPhase] {CurrentPhase} → {newPhase}");
//         OnPhaseChanged?.Invoke(newPhase);
//     }
// }
