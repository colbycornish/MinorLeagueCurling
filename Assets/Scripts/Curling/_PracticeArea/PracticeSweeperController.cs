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
    public Transform player;  // Assign your main character here
    public float followDistance = 0f;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
    public bool isLeftSweeper = false; // Assuming this is a left sweeper, adjust as needed
    public bool isRightSweeper = false; // Assuming this is a right sweeper, adjust as needed
    public Transform targetToLookAt;


    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.stoppingDistance = 0f;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // if (distance > followDistance)
        // {
        if (isLeftSweeper)
        {
            // Debug.Log($"[Distance] {distance}");
        }
        if (agent != null && agent.isOnNavMesh)
        {
            MoveCloserToPlayer();
        }
        // MoveCloserToPlayer();

        // }
        // else
        // {
        //     SitNextToPlayer();
        // }
    }

    // void FixedUpdate()
    // {
        
    // }

    void MoveCloserToPlayer()
    {
        // this works for the dog animations specifically
        // TODO: align the other creater animation labels.
        // if (animator.GetBool("Sit_b") == true)
        // {
        // animator.SetBool("Sit_b", false);
        // }

        // Vector3 playerForward = player.transform.forward; // player.position
        Vector3 playerPosition = player.position;
        // Vector3 sweeperPosition = playerPosition + playerForward * 6;
        Vector3 sweeperPositionL = new Vector3(-4, 0, 8);
        Vector3 sweeperPositionR = new Vector3(4, 0, 8);
        
        // Debug.Log($"[playerForward] {playerForward}");
        if (isLeftSweeper)
        {
            Vector3 leftSweeperPosition = playerPosition + sweeperPositionL;//new Vector3(sweeperPosition.x + 4, sweeperPosition.y, sweeperPosition.z);
            agent.SetDestination(leftSweeperPosition);
        }
        else if (isRightSweeper)
        {
            Vector3 rightSweeperPosition = playerPosition + sweeperPositionR; //new Vector3(sweeperPosition.x - 4, sweeperPosition.y, sweeperPosition.z);
            agent.SetDestination(rightSweeperPosition);
        }

        if (targetToLookAt != null)
        {
            transform.LookAt(targetToLookAt.position);
        }
        
        
        

        // float speed = agent.velocity.magnitude;
        // animator.SetFloat("Movement_f", speed);
    }

    // void SitNextToPlayer()
    // {
    //     agent.ResetPath();
    //     animator.SetFloat("Movement_f", 0f);
    //     animator.SetBool("Sit_b", true);
    // }

     


}
