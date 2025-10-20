using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class StoneSquareItemState : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private Image backgroundColor;
    [SerializeField] private RawImage faceRenderTexture;

    public void UpdateText(string name){
        if (textName != null){
            textName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}";
        }
    }

    public void UpdateImage(Texture renderTexture){
        if (faceRenderTexture != null){
            faceRenderTexture.texture = renderTexture;
        }
    }

    public void UpdateBackgroundColor(Image bc){
        if (backgroundColor != null){
            backgroundColor = bc;
        }
    }

    public void UpdateUI()
    {

    }
}