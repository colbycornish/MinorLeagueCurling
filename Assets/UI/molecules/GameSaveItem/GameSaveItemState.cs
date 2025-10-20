using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class GameSaveItemState : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textDate;
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private Image backgroundColor;
    [SerializeField] private RawImage imageRenderTexture;

    public void UpdateText(string name){
        textName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}";
    }

    public void UpdateImage(Texture renderTexture){
        imageRenderTexture.texture = renderTexture;
        // faceRenderTexture = renderTexture;
    }

    public void UpdateBackgroundColor(Image bc){
        backgroundColor = bc;
    }

    public void UpdateUI()
    {

    }
}