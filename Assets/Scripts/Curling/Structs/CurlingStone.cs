using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class CurlingStone : MonoBehaviour
{
    [Header("Basic Data")]
    public string title = "Basic Stone";
    public string description = "Just your basic curling stone.";
    public Image avatarImage;

    [Header("Ids")]
    [HideInInspector] public int teamId_i;
    [HideInInspector] public string teamId;
    [HideInInspector] public int stoneIndex;

    [Header("Status")]
    [HideInInspector] public bool isThrown = false;
    [HideInInspector] public bool isInPlay = false;
    [HideInInspector] public bool isSliding = false;

    [Header("Stats")]
    [HideInInspector] public float curlAmountCurrent = 0f;
    [HideInInspector] public float curlAmountInitial = 0f;
    [HideInInspector] public float launchForce = 0f;
    // [HideInInspector] public float primaryColor = 0f;
    
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
    }

    public void ResetStone(Vector3 position, Quaternion rotation)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = position;
        transform.rotation = rotation;
    }

    public bool IsStationary => rb != null && rb.linearVelocity.sqrMagnitude < 0.01f && rb.angularVelocity.sqrMagnitude < 0.01f;
}