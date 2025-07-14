using UnityEngine;
using UnityEngine.AI;

public class WanderController : MonoBehaviour
{
    // public Transform player;  // Assign your main character here
    public float followDistance = 5f;
    private NavMeshAgent agent;
    private Animator animator;
    public Transform destination1;
    public Transform destination2;
    private Transform currentDestination;

    void Start()
    {
        currentDestination = destination1;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, currentDestination.position);

        if (distance > followDistance)
        {
            MoveCloserToDestination();
        }
        else
        {
            ChangeDestination();
        }
    }

    void MoveCloserToDestination()
    {
        // if (animator.GetBool("Sit_b") == true)
        // {
        //     animator.SetBool("Sit_b", false);
        // }
        agent.SetDestination(currentDestination.position);

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Movement_f", speed);
    }

    // void SitNextToPlayer()
    // {
    //     agent.ResetPath();
    //     animator.SetFloat("Movement_f", 0f);
    //     animator.SetBool("Sit_b", true);
    // }

    void ChangeDestination()
    {
        agent.ResetPath();
        if (currentDestination = destination1)
        {
            currentDestination = destination2;
        }
        else
        {
            currentDestination = destination1;
        }
    }
    

}