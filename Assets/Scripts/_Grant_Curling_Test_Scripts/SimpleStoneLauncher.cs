using UnityEngine;

public class SimpleStoneLauncher : MonoBehaviour
{
    private Rigidbody rb;
    public float launchForce = 15f;
    private bool hasLaunched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!hasLaunched && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Launching stone!");
            rb.AddForce(Vector3.forward * launchForce, ForceMode.Impulse);
            hasLaunched = true;
        }
    }
}
