using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

// CharacterFullDisplayItem
// public enum CharacterFullDisplayItemState
// {
//     NoInfo,
//     Empty,
//     Highlighted,
// }

public class CharacterFullDisplayItem : MonoBehaviour
{
    // public Sprite icon;
    [Header("Components")]
    [SerializeField] private CharacterInfoDisplay infoDisplay;
    [SerializeField] private RawImage characterImage;
    [HideInInspector] private RectTransform rectTransform;
    [SerializeField] private GameObject equippedStonesArea;
    [SerializeField] private GameObject equippedBroomArea;
    [SerializeField] public CharacterEquipmentArea equipment;
    [SerializeField] private GameObject selectedBackground;

    [Header("Position")]
    [SerializeField] private bool isThrower = false;
    [SerializeField] private bool isLeftSweeper = false;
    [SerializeField] private bool isRightSweeper = false;

    [Header("Settings")]
    [SerializeField] private bool hasInfo = false;
    [SerializeField] private bool showEquipment = true;

    [Header("Visual State")]
    [SerializeField] private bool isSelected = false;
    [SerializeField] private bool isEditingEquipment = false;
    

    [Header("Shrink / Expand Settings")]
    [SerializeField] private float minWidth;
    [SerializeField] private float maxWidth;
    [SerializeField] public float shrinkDuration = 2f; // Time in seconds to complete shrinkage

    // public void UpdateText(string name){
    //     textName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}:";
    // }
    public void Start()
    {
        if (isThrower && equipment != null){
            equipment.useBrooms = false;
            equipment.useStones = true;
            equipment.broomArea.gameObject.SetActive(false);
            equipment.stoneArea.gameObject.SetActive(true);
        }
        if ((isLeftSweeper || isRightSweeper) && equipment != null){
            equipment.useBrooms = true;
            equipment.useStones = false;
            equipment.broomArea.gameObject.SetActive(true);
            equipment.stoneArea.gameObject.SetActive(false);
        }
        
    }
    

    public void UpdateUI()
    {
        
    }

    public void UpdateInfo(
        Character character = null
    )
    {
        if (character == null)
        {
            hasInfo = false;
            infoDisplay.UpdateInfo(
                name: "?????????",
                description: "unknown"
            );
            characterImage.color = Color.black;
        }
        else
        {   
            infoDisplay.UpdateInfo(
                character: character,
                name: character.fullName,
                // position: character.position,
                description: character.description
            );

            hasInfo = true;
            characterImage.color = Color.white; 
            
        }
    }

    public void UpdateRenderTexture(Texture renderTexture)
    {
        characterImage.texture = renderTexture;
    }

    public void SetAsUnSelected()
    {
        hasInfo = false;
        infoDisplay.UpdateInfo(
            name: "?????????",
            // position: character.position,
            description: "unknown"
        );
        characterImage.color = Color.black;
    }

    public void SetAsSelected()
    {
        hasInfo = true;
        characterImage.color = Color.white;   
    }

    public void SetSelected(bool status = false){
        if (status == true){
            StartCoroutine(Expand());
        }
        else {
            StartCoroutine(Shrink());
        }
    }


    public void SetHighlighted(bool status = false){
        if (selectedBackground != null){
            selectedBackground.SetActive(status);
        }
    }

    public void SetEquipmentInfo(
        int index = 0
    ){

    }

    public void SetSelectedEquipment(bool status = false, int index = 0){
        // if (status == true){
            // equipment.
        // }
        // if ()
    }


    public void UpdateEquipmentInfo(
        CurlingBroom broom = null,
        CurlingStone stone = null,
        int stoneIndex = 0
    )
    {
        if (isThrower)
        {
            equipment.UpdateStoneInfo(stone: stone, stoneIndex: stoneIndex);
        }
        else
        {   
            equipment.UpdateBroomInfo(broom: broom);
        }
    }

    



    // public void SetUnHighlighted(){
    //     if (selectedBackground != null){
    //         selectedBackground.SetActive(false);
    //     }
    // }



    /// Animations
    public IEnumerator Expand()
    {
        Vector3 target = new Vector3(.65f, .65f, .65f);
        float duration = 0.2f;

        Vector3 initialScale = characterImage.transform.localScale;
        float timer = 0f;

        while (timer < duration)
        {
            characterImage.transform.localScale = Vector3.Lerp(initialScale, target, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        characterImage.transform.localScale = target;
    }

    public IEnumerator Shrink()
    {
        Vector3 target = new Vector3(.5f, .5f, .5f);
        float duration = 0.2f;

        Vector3 initialScale = characterImage.transform.localScale;
        float timer = 0f;

        while (timer < duration)
        {
            characterImage.transform.localScale = Vector3.Lerp(initialScale, target, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        characterImage.transform.localScale = target;
    }

    
}