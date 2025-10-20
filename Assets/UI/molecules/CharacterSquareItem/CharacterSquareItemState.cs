using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CharacterSquareItemState : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private Image backgroundColor;
    [SerializeField] private RawImage faceRenderTexture;

    public void UpdateText(string name){
        textName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}";
    }

    public void UpdateFace(RenderTexture renderTexture){
        faceRenderTexture.texture = renderTexture;
    }

    public void UpdateBackgroundColor(Image bc){
        backgroundColor = bc;
    }

    public void UpdateUI()
    {

    }
}