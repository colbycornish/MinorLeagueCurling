using UnityEngine;
using System;

public class CurlingMatchPhaseManager : MonoBehaviour
{
    public static CurlingMatchPhaseManager Instance { get; private set; }
    public CurlingMatchPhase CurrentPhase { get; private set; }
    public event Action<CurlingMatchPhase> OnPhaseChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetPhase(CurlingMatchPhase newPhase)
    {
        Debug.Log($"[MatchPhase] → {newPhase}");
        if (CurrentPhase == newPhase) return;

        CurrentPhase = newPhase;
        Debug.Log($"[MatchPhase] → {newPhase}");
        OnPhaseChanged?.Invoke(newPhase);
    }
}
