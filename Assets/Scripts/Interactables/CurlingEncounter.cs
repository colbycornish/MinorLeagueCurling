using UnityEngine;
using System.Linq;
using UnityEngine.Playables;


/// <summary>
/// Teleports the player to a target scene and spawn ID, using a validated dropdown from a spawn database.
/// </summary>
public class CurlingEncounter : MonoBehaviour
{
    public string enemyGroupName = "Curlers";
    public string enemyGroupChallengeText = "Press E to start curling challenge. Press O to cancel.";
    private bool playerInRange = false;
    public CurlingCourseData curlingCourse;
    public KeyCode interactKey = KeyCode.X;
    public KeyCode challengeKey = KeyCode.E;
    public KeyCode cancelKey = KeyCode.O;
    public PlayableDirector courseIntroTimeline;
    

    private void Update()
    {
        if (playerInRange)
        {

            bool isDialogueActive = DialogueManager._instance.isDialogueActive;
            bool isNotificationActive = NotificationManager._instance.isNotificationActive;

            if (isNotificationActive == false && isDialogueActive == false)
            {
                CloseDialogue();
                OpenNotification();
            }
            if (isNotificationActive == true && Input.GetKeyDown(interactKey))
            {
                OpenDialogue();
                CloseNotification();
            }
            if (isDialogueActive == true && Input.GetKeyDown(cancelKey))
            {
                CloseDialogue();
                OpenNotification();
            }
            if (isDialogueActive == true && Input.GetKeyDown(challengeKey))
            {
                InitiateCurlingMatch();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            OpenNotification();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("💻 Player exited interaction range.");
            playerInRange = false;
            CloseNotification();
            CloseDialogue();
        }
    }


    public void PlayCourseIntroTimeline()
    {
        // timeline = GetComponent<PlayableDirector>();
        if (courseIntroTimeline != null)
        {
            courseIntroTimeline.Play();
        }
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
        CloseDialogue();
        Debug.Log("Initiate Curling!");
        PlayCourseIntroTimeline();
    }
}