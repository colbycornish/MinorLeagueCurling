using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages inputs for the Curling game.
/// It's unclear if inputs should be handled here, or at a lower level.
/// 
/// TODO: Research Input handling in Unity and decide if this is the right place.
/// </summary>


public class PracticeSweeperController : MonoBehaviour
{
    public Transform currentStone;  // Assign your main character here
    public float followDistance = 0f;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
    public bool isLeftSweeper = false; // Assuming this is a left sweeper, adjust as needed
    public bool isRightSweeper = false; // Assuming this is a right sweeper, adjust as needed
    public Transform targetToLookAt;
    public float pathUpdateFrequency = 0.5f;
    private float pathUpdateTimer = 0f; // How often to update the path


    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.stoppingDistance = 0f;
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, currentStone.position);

        if (agent != null && agent.isOnNavMesh)
        {
            MoveCloserToPlayer();
        }
    }



    void MoveCloserToPlayer()
    {

        Vector3 playerPosition = currentStone.position;
        Vector3 sweeperPositionL = new Vector3(-4, 0, 8);
        Vector3 sweeperPositionR = new Vector3(4, 0, 8);

        if (isLeftSweeper)
        {
            Vector3 leftSweeperPosition = playerPosition + sweeperPositionL;//
            // Vector3 leftSweeperPosition = new Vector3(playerPosition.x + 4, playerPosition.y, playerPosition.z + 6);
            agent.SetDestination(leftSweeperPosition);
        }
        else if (isRightSweeper)
        {
            Vector3 rightSweeperPosition = playerPosition + sweeperPositionR; //
            // Vector3 rightSweeperPosition = new Vector3(playerPosition.x - 4, playerPosition.y, playerPosition.z + 6);
            agent.SetDestination(rightSweeperPosition);
        }

        if (targetToLookAt != null)
        {
            transform.LookAt(targetToLookAt.position);
        }
    }
}
