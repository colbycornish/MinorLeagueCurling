using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class CurlingBroom : MonoBehaviour
{
    [Header("Basic Data")]
    public string id = "BROOM_BASIC";
    public string title = "Basic Broom";
    public string description = "Just your basic curling broom.";
    public Image avatarImage;

    [Header("Broom Parts")]
    public GameObject handleCap; // Optional: mesh or model 
    public GameObject handle; // Optional: mesh or model 
    public GameObject brush; // Optional: mesh or model 
    public GameObject burshHairs; // Optional: mesh or model 

    // [Header("Broom Part Classes")]
    // public CurlingBroomHandleCap handleCapController;
    // public CurlingBroomHandle handleController;
    // public CurlingBroomBrushCap brushController;
    // public CurlingBroomBrushHair brushHairController;

    [Header("Hand Positions")]
    public GameObject topHandHold; // Optional: mesh or model 
    public GameObject bottomHandHold; // Optional: mesh or model 

    [Header("Game Object")]
    public Rigidbody rb; // Rigidbody to apply force / detect motion
    public GameObject visual; // Optional: mesh or model 

    [Header("Special Abilities")]
    public bool hasSpecialAbilities = false;
    // public List<CurlingSpecialAbility> specialAbilities;

    [Header("Editable")]
    public bool isHandleCapEditable = false;
    public bool isHandleEditable = false;
    public bool isBrushEditable = false;
    public bool isBrushHairsEditable = false;



    private void Awake()
    {
        // TODO: Create the below classes, then activate this
        // if (handleCap != null) handleCapController = handleCap.GetComponent<CurlingBroomHandleCap>();
        // if (handle != null) handleController = handleCap.GetComponent<CurlingBroomHandle>();
        // if (brush != null) brushController = handleCap.GetComponent<CurlingBroomBrush>();
        // if (burshHairs != null) brushHairController = handleCap.GetComponent<CurlingBroomBrushHair>();        
    }


    public void AddAttachment(

    )
    {
        
    }


}