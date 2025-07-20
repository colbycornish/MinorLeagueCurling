using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CorneliusInteraction : MonoBehaviour
{
    public string npcName = "Cornelius";
    public GameObject promptUI; // UI element like "Press X to talk to Dan"
    public GameObject dialogueUI; // The dialogue window
    public TextMeshProUGUI dialogueText; // The main text box
    public GameObject whiskeyOption;
    public GameObject beerOption;
    public GameObject closePromptText; // "Press X to Close" type of thing

    private bool playerInRange = false;
    private bool dialogueActive = false;
    private bool responseDisplayed = false; // will be used to trigger when the closePromptText is displayed

    void Start()
    {
        if (promptUI != null)
            promptUI.SetActive(false);

        if (dialogueUI != null)
            dialogueUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.X))
        {
            if (!dialogueActive)
            {
                StartDialogue();
            }

            else if (responseDisplayed)
            {
                CloseDialogue();
            }
        }

        if (dialogueActive && !responseDisplayed)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Respond(true);
            }

            else if (Input.GetKeyDown(KeyCode.B))
            {
                Respond(false);
            }
        }
    }

    void StartDialogue()
    {
        promptUI.SetActive(false);
        dialogueUI.SetActive(true);
        dialogueText.text = "Do you prefer whisky or beer?";
        dialogueActive = true;
        responseDisplayed = false;

        whiskeyOption.SetActive(true);
        beerOption.SetActive(true);
        closePromptText.SetActive(false);
    }

    void Respond(bool likesWhiskey)
    {
        if (likesWhiskey)
            dialogueText.text = "American, eh? Yeah, I see that text box. It's whisky in Scotland, laddie. Good choice - cheers.";
        else
            dialogueText.text = "Were you in a fraternity or something?";

        whiskeyOption.SetActive(false);
        beerOption.SetActive(false);

        responseDisplayed = true;
        closePromptText.SetActive(true); // Show the "Press X to close" prompt

    }

    void CloseDialogue()
    {
        dialogueUI.SetActive(false);
        closePromptText.SetActive(false);
        dialogueActive = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            promptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            promptUI.SetActive(false);
            dialogueUI.SetActive(false);
            dialogueActive = false;
            responseDisplayed = false;
        }
    }
}