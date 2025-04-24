using UnityEngine;

public class SnapRotation
{
    private Quaternion targetRotation;
    private float rotationTimer;
    private readonly float rotationDuration;
    private bool isTurning;

    public SnapRotation(float rotationDuration)
    {
        this.rotationDuration = rotationDuration;
        targetRotation = Quaternion.identity;
    }

    public bool IsTurning => isTurning;

    public void StartTurn(Quaternion currentRotation, float angleDelta)
    {
        if (isTurning) return;

        targetRotation = Quaternion.Euler(0, currentRotation.eulerAngles.y + angleDelta, 0);
        isTurning = true;
        rotationTimer = 0f;
    }

    public Quaternion UpdateRotation(Quaternion currentRotation, float deltaTime)
    {
        if (!isTurning)
            return currentRotation;

        rotationTimer += deltaTime;
        float t = Mathf.Clamp01(rotationTimer / rotationDuration);
        Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, t);

        if (t >= 1f)
        {
            isTurning = false;
            rotationTimer = 0f;
        }

        return newRotation;
    }
}