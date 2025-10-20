using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CourseItemState : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private Image backgroundColor;
    [SerializeField] private RawImage courseRenderTexture;

    public void UpdateText(string name){
        textName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}";
    }

    public void UpdateImage(Texture renderTexture){
        courseRenderTexture.texture = renderTexture;
        // faceRenderTexture = renderTexture;
    }

    public void UpdateBackgroundColor(Image bc){
        backgroundColor = bc;
    }

    public void UpdateUI()
    {

    }
}