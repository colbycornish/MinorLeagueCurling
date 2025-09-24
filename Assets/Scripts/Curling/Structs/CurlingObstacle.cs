using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class CurlingObstacle : MonoBehaviour
{
    [Header("Basic Data")]
    public string title = "Basic Obstacle";
    public string description = "Just your basic curling obstacle.";
    public Image avatarImage;

    [Header("Status")]
    public bool hasInteractionFx = false;
    [HideInInspector] public bool isOnFire = false;
    [HideInInspector] public bool isSlippery = false;


    [Header("Animation/FX")]
    public GameObject impactFx;
    public GameObject fireFx;
    [HideInInspector] public bool animationsAreActive = false;
    [HideInInspector] public bool hasAnimations = false;
    [HideInInspector] public bool hasImpactFX = false;
    [HideInInspector] public bool hasFireFX = false;

    [Header("Model")]
    public Rigidbody rb; // Rigidbody to apply force / detect motion
    public GameObject visual; // Optional: mesh or model 
    // 
    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (impactFx != null) { hasImpactFX = true; }
        if (fireFx != null) { hasFireFX = true; }
        if (hasFireFX || hasImpactFX) { hasAnimations = true; }
        DeactivateFx();
    }

    public void DeactivateFx()
    {
        animationsAreActive = false;
        if (impactFx != null) { impactFx.SetActive(false); }   
        if (fireFx != null){ fireFx.SetActive(false); }   
    }

    public void ActivateFx()
    {
        animationsAreActive = true;
        if (impactFx != null) { impactFx.SetActive(true); }   
        if (fireFx != null){ fireFx.SetActive(true); }   
    }

}


