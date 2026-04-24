// 11/21/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using Animancer;
using UnityEngine;

namespace AnimalNPC
{
    public class AnimalStateMachine : MonoBehaviour
    {
        public enum AnimalState
        {
            Barking,
            Dead,
            Eating,
            Idle,
            Moving,
            Patrolling,
            Sleeping
        }

        public AnimancerComponent animancer;
        public AnimalState currentState;

        private void Start()
        {
            ChangeState(AnimalState.Idle);
        }

        public void ChangeState(AnimalState newState)
        {
            currentState = newState;

            switch (currentState)
            {
                case AnimalState.Idle:
                    PlayAnimation("Idle");
                    break;
                case AnimalState.Moving:
                    PlayAnimation("Walk");
                    break;
                case AnimalState.Patrolling:
                    PlayAnimation("Patrol");
                    break;
                case AnimalState.Dead:
                    PlayAnimation("Death");
                    break;
                case AnimalState.Sleeping:
                    PlayAnimation("Sleep");
                    break;
                case AnimalState.Eating:
                    PlayAnimation("Eat");
                    break;
                case AnimalState.Barking:
                    PlayAnimation("Bark");
                    break;
            }
        }

        private void PlayAnimation(string animationName)
        {
            // var clip = animancer.Animator.runtimeAnimatorController.animationClips.FirstOrDefault(c => c.name == animationName);
            // if (clip != null)
            // {
            //     animancer.Play(clip);
            // }
            // else
            // {
            //     Debug.LogWarning($"Animation '{animationName}' not found!");
            // }
            // [SerializeField] private TransitionAsset _Animation;
        }
    }
}