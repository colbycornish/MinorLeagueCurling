using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;



public class EquippedBroomDisplayImageItemState : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textDesc;
    [SerializeField] private Image backgroundColor;
    [SerializeField] private RawImage renderTexture;

    public void UpdateName(string name){
        textName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}";
    }

    public void UpdateDesc(string desc){
        textDesc.GetComponent<TMPro.TextMeshProUGUI>().text = $"{desc}";
    }

    public void UpdateImage(Texture newTexture){
        if (renderTexture != null){
            renderTexture.texture = newTexture;
        }
    }

    public void UpdateBackgroundColor(Image bc){
        backgroundColor = bc;
    }


    
}

