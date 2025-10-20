using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class LoadingText : MonoBehaviour
{
    [Header("Game Object")]
    public List<string> loadingMessages;
    [SerializeField] private TextMeshProUGUI textBox;
    



    private void Awake()
    {
        // TODO: Create the below classes, then activate this
        // if (handleCap != null) handleCapController = handleCap.GetComponent<CurlingBroomHandleCap>();
        // if (handle != null) handleController = handleCap.GetComponent<CurlingBroomHandle>();
        // if (brush != null) brushController = handleCap.GetComponent<CurlingBroomBrush>();
        // if (burshHairs != null) brushHairController = handleCap.GetComponent<CurlingBroomBrushHair>();        
    }


    public void SelectLoadingText()
    {
        
        loadingMessages.Add("Originally, the sport of curling did not make use of exploding obstacles. This is widely viewed as a mistake.");
        loadingMessages.Add("In 1688, King Henry IV declared that all curling locations must be open on Sundays, which infuriated the Church");
        loadingMessages.Add("During the California Gold Rush, sweepers used dynamite to influence the path of the stone.");
    }


}