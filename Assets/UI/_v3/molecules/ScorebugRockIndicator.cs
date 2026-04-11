using UnityEngine;

namespace CurlingUI.v3 {
    public class ScorebugRockIndicator: MonoBehaviour
    {

        public enum RockIndicatorState
        {
            Default,
            Active,
            GoodThrow,
            BadThrow
        }

        public RockIndicatorState currentState = RockIndicatorState.Default;
        public GameObject Default;
        public GameObject Active;
        public GameObject GoodThrow;
        public GameObject BadThrow;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            UpdateVisuals();
        }

        public void SetState(RockIndicatorState newState)
        {
            currentState = newState;
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            Default.SetActive(currentState == RockIndicatorState.Default);
            Active.SetActive(currentState == RockIndicatorState.Active);
            GoodThrow.SetActive(currentState == RockIndicatorState.GoodThrow);
            BadThrow.SetActive(currentState == RockIndicatorState.BadThrow);
        }

    }
}