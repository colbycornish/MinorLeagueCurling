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

    // [Header("Settings")]
    [SerializeField] private bool hasInfo = false;

    [Header("Shrink / Expand Settings")]
    [SerializeField] private float minWidth;
    [SerializeField] private float maxWidth;
    [SerializeField] public float shrinkDuration = 2f; // Time in seconds to complete shrinkage

    // public void UpdateText(string name){
    //     textName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}:";
    // }

    

    public void UpdateUI()
    {
        
    }

    public void UpdateRenderTexture(Texture renderTexture)
    {
        characterImage.texture = renderTexture;
    }

    public void UpdateInfo(
        Character character = null
    )
    {
        if (character == null)
        {
            SetAsUnSelected();
        }
        else
        {
            SetAsSelected();
            infoDisplay.UpdateInfo(
                character: character,
                name: character.fullName,
                // position: character.position,
                description: character.description
            );
        }
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