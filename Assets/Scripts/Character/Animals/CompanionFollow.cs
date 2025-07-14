using UnityEngine;
using UnityEngine.AI;

public class CompanionFollow : MonoBehaviour
{
    public Transform player;  // Assign your main character here
    public float followDistance = 5f;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > followDistance)
        {
            MoveCloserToPlayer();
        }
        else
        {
            SitNextToPlayer();
        }
    }

    void MoveCloserToPlayer()
    {
        // this works for the dog animations specifically
        // TODO: align the other creater animation labels.
        if (animator.GetBool("Sit_b") == true)
        {
            animator.SetBool("Sit_b", false);
        }
        agent.SetDestination(player.position);

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Movement_f", speed);
    }

    void SitNextToPlayer()
    {
        agent.ResetPath();
        animator.SetFloat("Movement_f", 0f);
        animator.SetBool("Sit_b", true);
    }
    

}