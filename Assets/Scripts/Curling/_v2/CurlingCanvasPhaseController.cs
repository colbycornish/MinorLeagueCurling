using UnityEngine;
using System.Collections.Generic;

public class CurlingCanvasPhaseController : MonoBehaviour
{
    [System.Serializable]
    public class CurlingPhaseCanvasPair
    {
        public CurlingMatchPhase phase;
        public GameObject canvas;
    }

    public List<CurlingPhaseCanvasPair> phaseCanvases;

    private Dictionary<CurlingMatchPhase, GameObject> canvasMap;

    private void Awake()
    {
        canvasMap = new Dictionary<CurlingMatchPhase, GameObject>();
        foreach (var pair in phaseCanvases)
        {
            if (!canvasMap.ContainsKey(pair.phase))
                canvasMap[pair.phase] = pair.canvas;
        }
    }

    private void OnEnable()
    {
        if (CurlingMatchPhaseManager.Instance == null) return;
        CurlingMatchPhaseManager.Instance.OnPhaseChanged += UpdateCanvases;
    }

    private void OnDisable()
    {
        if (CurlingMatchPhaseManager.Instance == null) return;
        CurlingMatchPhaseManager.Instance.OnPhaseChanged -= UpdateCanvases;
    }

    private void UpdateCanvases(CurlingMatchPhase phase)
    {
        
        // foreach (var kvp in canvasMap)
        // {

        //     kvp.Value.SetActive(kvp.Key == phase);
        // }
    }
}
