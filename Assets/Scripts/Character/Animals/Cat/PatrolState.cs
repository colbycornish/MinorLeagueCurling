using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AnimalControllers.Cat
{
    public class PatrolState : MonoBehaviour
    {
        [Header("Components")]
        public CatController animalController;
        public NavMeshAgent agent;

        [Header("Patrol State")]
        public List<Transform> patrolPoints;
        public float stoppingDistance = 0.5f; // How close the agent needs to be to a waypoint
        public float waitTimeAtWaypoint = 2f; // Time to wait at each waypoint


        [Header("Waiting State")]
        private float waitTimer;
        private bool isWaiting = false;

        [Header("Settings")]
        public int containmentRadius;
        public bool isDead = false;

        // isShaking
        // isWalking
        // isRunning
        // isDead

        void Start()
        {
            // animator = GetComponent<Animator>();
            // agent = GetComponent<NavMeshAgent>();
            if (patrolPoints.Count == 0)
            {
                Debug.LogError("Patrol points not assigned!");
                enabled = false; // Disable script if no points
                return;
            }
            SetNextDestination();
        }

        


        void Update()
        {
            if (isDead)
            {
                return;
            }
            if (agent.remainingDistance <= stoppingDistance && !agent.pathPending)
            {
                if (!isWaiting)
                {
                    isWaiting = true;
                    waitTimer = waitTimeAtWaypoint;

                }
                else
                {
                    waitTimer -= Time.deltaTime;
                    if (waitTimer <= 0)
                    {
                        isWaiting = false;
                        SetNextDestination();
                    }
                }
            }
            else
            {
                Move();
            }
        }

        void SetNextDestination()
        {
            int randomIndex = UnityEngine.Random.Range(0, patrolPoints.Count);
            Transform chosenPatrolPoint = patrolPoints[randomIndex];
            agent.SetDestination(chosenPatrolPoint.position);
        }

        public void Walking()
        {
            animalController.animator.SetBool("isWalking", true);
        }

        public void AnimalSound()
        {
            // Play animal sound
        }

        public void Move()
        {
            animalController.animator.SetBool("isWalking", true);
            animalController.animator.SetBool("isShaking", false);
            // animator.SetBool("isRunning", true);
        }
    }
}

