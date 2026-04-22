using UnityEngine;
using System.Linq;
using UnityEngine.Playables;
using UnityEngine.InputSystem;


/// <summary>
/// Triggers the beginning of a curling encounter!
/// </summary>
public class CurlingEncounter : MonoBehaviour
{
    public string enemyGroupName = "Curlers";
    public string enemyGroupChallengeText = "Press E to start curling challenge. Press O to cancel.";
    private bool playerInRange = false;
    public CurlingCourseData curlingCourse;
    public CurlingTeam teamAway;
    public CurlingTeam teamHome;
    // public KeyCode interactKey = KeyCode.X;
    // public KeyCode challengeKey = KeyCode.E;
    // public KeyCode cancelKey = KeyCode.O;
    public PlayableDirector courseIntroTimeline;
    private InputAction interactAction;
    private InputAction prevAction;


    void Awake()
    {
        interactAction = InputSystem.actions.FindAction("Interact", true);
        prevAction = InputSystem.actions.FindAction("Previous", true);
    }

    protected virtual void OnEnableInteraction()
    {
        Debug.Log("Interaction Actions Enabled");
        interactAction.performed += OnUpdateDialogue;
        interactAction.Enable();
 
        prevAction.performed += OnCloseDialogue;
        prevAction.Enable();
    }

    protected virtual void OnDisableInteraction()
    {
        Debug.Log("Interaction Actions DISABLED");
        interactAction.performed -= OnUpdateDialogue;
        interactAction.Disable();

        prevAction.performed -= OnCloseDialogue;
        prevAction.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("💻 Player Entered interaction range.");
            playerInRange = true;
            OnEnableInteraction();
            OpenNotification();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("💻 Player exited interaction range.");
            playerInRange = false;
            OnDisableInteraction();
            CloseNotification();
            CloseDialogue();
            
        }
    }

    // private void Update()
    // {
    //     if (playerInRange)
    //     {

    //         bool isDialogueActive = DialogueManager._instance.isDialogueActive;
    //         bool isNotificationActive = NotificationManager._instance.isNotificationActive;

    //         if (isNotificationActive == false && isDialogueActive == false)
    //         {
    //             CloseDialogue();
    //             OpenNotification();
    //         }
    //         if (isNotificationActive == true && Input.GetKeyDown(interactKey))
    //         {
    //             OpenDialogue();
    //             CloseNotification();
    //         }
    //         if (isDialogueActive == true && Input.GetKeyDown(cancelKey))
    //         {
    //             CloseDialogue();
    //             OpenNotification();
    //         }
    //         if (isDialogueActive == true && Input.GetKeyDown(challengeKey))
    //         {
    //             InitiateCurlingMatch();
    //         }
    //     }
    // }

    

    


    public void PlayCourseIntroTimeline()
    {
        // timeline = GetComponent<PlayableDirector>();
        if (courseIntroTimeline != null)
        {
            courseIntroTimeline.Play();
        }
    }

    private void OnUpdateDialogue(InputAction.CallbackContext obj)
    {
        Debug.Log("Interaction Action Registered");
        bool isDialogueActive = DialogueManager._instance.isDialogueActive;
        bool isNotificationActive = NotificationManager._instance.isNotificationActive;

        if (isNotificationActive == false && isDialogueActive == false)
        {
            Debug.Log("Openning Notification");
            CloseDialogue();
            OpenNotification();
        }
        if (isNotificationActive == true && isDialogueActive == false)
        {
            Debug.Log("Openning Dialogue");
            OpenDialogue();
            CloseNotification();
        }
        if (isDialogueActive == true)
        {
            Debug.Log("Starting Curling Match");
            OnDisableInteraction();
            CloseDialogue();
            InitiateCurlingMatch();
            
        }
        
    } 

    private void OnCloseDialogue(InputAction.CallbackContext obj)
    {
        CloseDialogue();
    } 
    



    /// Notification Controls
    public void OpenNotification()
    {
        NotificationManager._instance.NewNotification(enemyGroupName, "X");
        NotificationManager._instance.ExpandNotification();
    }

    public void CloseNotification()
    {   
        NotificationManager._instance.MinimizeNotification();
    }

    /// Dialogue Controls
    public void OpenDialogue()
    {
        DialogueManager._instance.NewDialogue(
            enemyGroupName,
            enemyGroupChallengeText,
            "O"
        );
        NotificationManager._instance.MinimizeNotification();
    }

    public void CloseDialogue()
    {
        DialogueManager._instance.MinimizeDialogue();
    }


    /// Initiate the Curling Match
    /// utility
    public void InitiateCurlingMatch()
    {
        // CloseDialogue();
        // Debug.Log("Initiate Curling!");
        // PlayCourseIntroTimeline();
        CurlingPreGameSetupManagerV2._instance.demoCurlingCourseData = curlingCourse;
        CurlingPreGameSetupManagerV2._instance.demoTeamHome = teamHome;
        CurlingPreGameSetupManagerV2._instance.demoTeamAway = teamAway;
        CurlingPreGameSetupManagerV2._instance.StartDemoCurlingGame();
    }
}