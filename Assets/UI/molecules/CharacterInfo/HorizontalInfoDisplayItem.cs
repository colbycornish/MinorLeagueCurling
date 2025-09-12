using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class HorizontalInfoDisplayItem : MonoBehaviour
{
    // public Sprite icon;
    [Header("Components")]
    [SerializeField] private GameObject icon;
    [SerializeField] private CharacterInfoDisplay infoDisplay;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textPosition;
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private GameObject infoArea;
    [SerializeField] private GameObject statsArea;
    [SerializeField] private GameObject actionButtonsArea;
    [HideInInspector] private RectTransform rectTransform;

    [Header("Shrink / Expand Settings")]
    [SerializeField] private float minWidth;
    [SerializeField] private float maxWidth;
    [SerializeField] public float shrinkDuration = 2f; // Time in seconds to complete shrinkage


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        // initialWidth = rectTransform.sizeDelta.x;
        
    }

    //
    public void Update()
    {
        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     StartCoroutine(Expand());
            
        // }
        // if (Input.GetKeyDown(KeyCode.V))
        // {
        //     StartCoroutine(Shrink());
            
        // }

    }

    public void Init()
    {

    }

    public void UpdateInfo(
        Character character = null
    )
    {
        if (character == null)
        {
            // SetAsUnSelected();
        }
        else
        {
            // SetAsSelected();
            infoDisplay.UpdateInfo(
                character: character,
                name: character.fullName,
                // position: character.position,
                description: character.description
            );
        }
    }

    // private void UpdateName(string name)
    // {
    //     textName.GetComponent<TMPro.TextMeshProUGUI>().text = name;
    // }

    // private void UpdatePosition(string position)
    // {
    //     textPosition.GetComponent<TMPro.TextMeshProUGUI>().text = position;
    // }

    // private void UpdateDescription(string description)
    // {
    //     textDescription.GetComponent<TMPro.TextMeshProUGUI>().text = description;
    // }

    private void UpdatePositionIcon(Sprite icon)
    {

    }

    
    public IEnumerator Expand()
    {
        float timer = 0f;
        while (timer < shrinkDuration)
        {
            timer += Time.deltaTime;
            float newWidth = Mathf.Lerp(minWidth, maxWidth, timer / shrinkDuration);
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
            yield return null; // Wait for the next frame
        }
        // Ensure the width is exactly the targetWidth at the end
        rectTransform.sizeDelta = new Vector2(maxWidth, rectTransform.sizeDelta.y);
    }
    
    public IEnumerator Shrink()
    {
        float timer = 0f;
        while (timer < shrinkDuration)
        {
            timer += Time.deltaTime;
            float newWidth = Mathf.Lerp(maxWidth, minWidth, timer / shrinkDuration);
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
            yield return null; // Wait for the next frame
        }
        // Ensure the width is exactly the targetWidth at the end
        rectTransform.sizeDelta = new Vector2(minWidth, rectTransform.sizeDelta.y);
    }
}
