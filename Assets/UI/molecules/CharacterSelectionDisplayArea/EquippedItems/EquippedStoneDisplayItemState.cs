using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;



public class EquippedStoneDisplayItemState : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private Image backgroundColor;
    [SerializeField] private RawImage renderTexture;

    public void UpdateText(string name){
        textName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}";
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