using UnityEngine;
using System.Collections.Generic;


public class CurlingStone : MonoBehaviour
{
    public int teamId;
    public int stoneIndex;

    [HideInInspector] public bool isThrown = false;
    [HideInInspector] public bool isInPlay = false;
    [HideInInspector] public bool isSliding = false;
    
    [Header("References")]
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

    public bool IsStationary => rb.linearVelocity.sqrMagnitude < 0.01f && rb.angularVelocity.sqrMagnitude < 0.01f;
}