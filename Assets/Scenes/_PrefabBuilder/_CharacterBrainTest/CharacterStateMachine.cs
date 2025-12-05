// 11/21/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using Animancer;
using UnityEngine;

namespace CharacterNPC
{
    public class CharacterStateMachine : MonoBehaviour
    {
        public enum CharacterState
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
        public CharacterState currentState;

        private void Start()
        {
            ChangeState(CharacterState.Idle);
        }

        public void ChangeState(CharacterState newState)
        {
            currentState = newState;

            switch (currentState)
            {
                case CharacterState.Idle:
                    PlayAnimation("Idle");
                    break;
                case CharacterState.Moving:
                    PlayAnimation("Walk");
                    break;
                case CharacterState.Patrolling:
                    PlayAnimation("Patrol");
                    break;
                case CharacterState.Dead:
                    PlayAnimation("Death");
                    break;
                case CharacterState.Sleeping:
                    PlayAnimation("Sleep");
                    break;
                case CharacterState.Eating:
                    PlayAnimation("Eat");
                    break;
                case CharacterState.Barking:
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