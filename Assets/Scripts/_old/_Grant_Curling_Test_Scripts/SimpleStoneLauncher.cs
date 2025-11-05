// using UnityEngine;

// public class SimpleStoneLauncher : MonoBehaviour
// {
//     public Rigidbody stoneRb;                      // Assign in Inspector
//     public Transform directionPivot;               // Assign the same pivot you're rotating
//     public float launchForce = 50f;                // Adjust as needed
//     private bool hasLaunched = false;

//     void Update()
//     {
//         if (!hasLaunched && Input.GetKeyDown(KeyCode.Space))
//         {
//             Vector3 launchDirection = directionPivot.forward;
//             stoneRb.AddForce(launchDirection * launchForce, ForceMode.Impulse);
//             hasLaunched = true;
//             Debug.Log("🚀 Stone launched!");
//         }
//     }
// }
