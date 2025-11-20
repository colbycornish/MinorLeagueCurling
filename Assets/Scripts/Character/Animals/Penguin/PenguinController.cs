using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AnimalControllers.Penguin
{
    public class PenguinController : MonoBehaviour
    {
        [Header("Components")]
        public Animator animator;
        public NavMeshAgent agent;

        [Header("Patrol State")]
        public List<Transform> patrolPoints;
        public float stoppingDistance = 0.5f; // How close the agent needs to be to a waypoint
        public float waitTimeAtWaypoint = 2f; // Time to wait at each waypoint

        public Transform homePosition;
        public Transform otherPosition;
        private Transform targetPosition;

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
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
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
                    Shake();

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
            animator.SetBool("isWalking", true);
        }

        public void AnimalSound(){}

        public void Move()
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isShaking", false);
            // animator.SetBool("isRunning", true);
        }

        public void Shake()
        {
            animator.SetBool("isShaking", true);
        }

        public void Dead()
        {
            animator.SetBool("isDead", true);
            isDead = true;
        }
    }
}


// void Update()
//         {
//             if (isDead)
//             {
//                 return;
//             }
//             else
//             {
//                 if (targetPosition == null)
//                 {
//                     Wander();
//                 }
//                 if (Vector3.Distance(transform.position, homePosition.position) > containmentRadius)
//                 {
//                     ReturnHome();
//                     Move();
//                 }
//             }
//         }

// using UnityEngine;
// using UnityEngine.AI; // Required for NavMeshAgent

// public class PatrolAgent : MonoBehaviour
// {
//     public Transform[] patrolPoints; // Array to hold your waypoint GameObjects
//     public float stoppingDistance = 0.5f; // How close the agent needs to be to a waypoint
//     public float waitTimeAtWaypoint = 2f; // Time to wait at each waypoint

//     private NavMeshAgent agent;
//     private int currentPatrolIndex = 0;
//     private float waitTimer;
//     private bool isWaiting = false;

//     void Start()
//     {
//         agent = GetComponent<NavMeshAgent>();
//         if (patrolPoints.Length == 0)
//         {
//             Debug.LogError("Patrol points not assigned!");
//             enabled = false; // Disable script if no points
//             return;
//         }
//         SetNextDestination();
//     }

//     void Update()
//     {
//         if (agent.remainingDistance <= stoppingDistance && !agent.pathPending)
//         {
//             if (!isWaiting)
//             {
//                 isWaiting = true;
//                 waitTimer = waitTimeAtWaypoint;
//             }
//             else
//             {
//                 waitTimer -= Time.deltaTime;
//                 if (waitTimer <= 0)
//                 {
//                     isWaiting = false;
//                     SetNextDestination();
//                 }
//             }
//         }
//     }

//     void SetNextDestination()
//     {
//         currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Cycle through points
//         agent.SetDestination(patrolPoints[currentPatrolIndex].position);
//     }

//     // Optional: Visualize the path in the editor
//     void OnDrawGizmos()
//     {
//         if (patrolPoints == null || patrolPoints.Length < 2) return;

//         Gizmos.color = Color.yellow;
//         for (int i = 0; i < patrolPoints.Length; i++)
//         {
//             if (patrolPoints[i] != null)
//             {
//                 Gizmos.DrawSphere(patrolPoints[i].position, 0.3f); // Draw a sphere at each waypoint
//                 if (i < patrolPoints.Length - 1 && patrolPoints[i + 1] != null)
//                 {
//                     Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position); // Draw lines between waypoints
//                 }
//             }
//         }
//         // Draw line from last to first point for a looping path
//         if (patrolPoints[patrolPoints.Length - 1] != null && patrolPoints[0] != null)
//         {
//             Gizmos.DrawLine(patrolPoints[patrolPoints.Length - 1].position, patrolPoints[0].position);
//         }
//     }
// }