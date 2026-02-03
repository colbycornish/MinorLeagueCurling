using UnityEngine;

public class SlidingDoorController : MonoBehaviour
{
    private Animator doorAnimator;

    // Optional: Public variables for trigger names if you want to easily change them
    public string openTriggerName = "OpenDoor";
    public string closeTriggerName = "CloseDoor";

    void Awake()
    {
        doorAnimator = GetComponent<Animator>();
    }

    // Example for triggering on player proximity
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming your player has the "Player" tag
        {
            Debug.Log("Player Entered Area - Opening door");
            // doorAnimator.SetTrigger(openTriggerName);
            OpenDoor();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Left Area - Closing door");
            CloseDoor();
        }
    }


    void OpenDoor()
    {
        Debug.Log("OpenDoor method called");
        doorAnimator.SetBool("openDoor", true);
    }

    void CloseDoor()
    {
        Debug.Log("CloseDoor method called");
        doorAnimator.SetBool("openDoor", false);
    }

    // Example for triggering on key press
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.E)) // Press 'E' to open/close
        // {
        //     // You might need more complex logic here to determine if it should open or close
        //     // For simplicity, this example just toggles
        //     if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("SlideClose"))
        //     {
        //         Debug.Log("E key pressed - Opening door");
        //         OpenDoor();
        //     }
        //     else if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("SlideOpen"))
        //     {
        //         Debug.Log("E key pressed - Closing door");
        //         CloseDoor();
        //     }
        // }
    }
}