using System.Collections.Generic;
using UnityEngine;

public class CurlingEndGameManagerV2 : MonoBehaviour
{
    public GameObject targetZone;
    private List<GameObject> stonesThisEnd = new List<GameObject>();
    private int[] endScore = new int[2];

    public void ResetCurlingGame()
    {
        stonesThisEnd.Clear();
    }

    public void AddStone(GameObject stone)
    {
        stonesThisEnd.Add(stone);
    }

    public void CalculateScore()
    {
        stonesThisEnd.Sort((a, b) =>
            Vector3.Distance(Vector3.zero, a.transform.position)
            .CompareTo(Vector3.Distance(Vector3.zero, b.transform.position)));

        endScore = new int[2];
        if (stonesThisEnd.Count > 0)
        {
            CurlingStone stone = stonesThisEnd[0].GetComponent<CurlingStone>();
            endScore[stone.teamId]++;
        }

        Debug.Log($"Scoring End: Team A: {endScore[0]}, Team B: {endScore[1]}");
    }

    public int[] GetScore() => endScore;
}
