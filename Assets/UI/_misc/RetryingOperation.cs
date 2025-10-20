 using System.Collections;
using UnityEngine;

public class RetryingOperation : MonoBehaviour
{
    private bool operationSuccessful = false;

    IEnumerator AttemptOperationWithRetry(int maxRetries, float delayBetweenRetries)
    {
        for (int i = 0; i < maxRetries; i++)
        {
            Debug.Log($"Attempting operation (Retry {i + 1}/{maxRetries})...");
            // Simulate an operation that might fail
            operationSuccessful = SimulateOperation(); 

            if (operationSuccessful)
            {
                Debug.Log("Operation successful!");
                yield break; // Exit the coroutine if successful
            }
            else
            {
                Debug.Log("Operation failed. Retrying in " + delayBetweenRetries + " seconds.");
                yield return new WaitForSeconds(delayBetweenRetries); // Wait before retrying
            }
        }

        Debug.Log("Operation failed after multiple retries.");
    }

    // Simulate an operation that has a chance to fail
    private bool SimulateOperation()
    {
        // For demonstration, let's say it succeeds 50% of the time
        return Random.Range(0, 2) == 0; 
    }

    void Start()
    {
        StartCoroutine(AttemptOperationWithRetry(3, 2f)); // Try 3 times with a 2-second delay
    }
}