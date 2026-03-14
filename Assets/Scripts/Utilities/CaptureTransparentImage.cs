using UnityEngine;
using System.IO;

public class CaptureTransparentImage : MonoBehaviour
{
    public Camera captureCamera; // Drag your dedicated camera here in the Inspector
    public int captureWidth = 1024;
    public int captureHeight = 1024;

    public void CaptureScreenshot()
    {
        // Create a new render texture
        RenderTexture rt = new RenderTexture(captureWidth, captureHeight, 24, RenderTextureFormat.ARGB32);
        captureCamera.targetTexture = rt;
        
        // Render the camera view to the render texture
        captureCamera.Render();

        // Set the active render texture to the new one for reading pixels
        RenderTexture.active = rt;
        
        // Create a new Texture2D with ARGB32 format to support transparency
        Texture2D screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.ARGB32, false);
        screenShot.ReadPixels(new Rect(0, 0, captureWidth, captureHeight), 0, 0);
        screenShot.Apply();

        // Clean up
        captureCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // Encode to PNG and save to file
        byte[] bytes = screenShot.EncodeToPNG();

        string subDirectory = "UI/Textures/Avatars/ScreenCaptures/";
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = subDirectory + "screenshot_" + timestamp + ".png";

        // string directoryPath = Path.Combine(Application.dataPath, "/UI/Textures/Avatars/ScreenCaptures/");
        // Debug.Log("Directory Path: " + directoryPath);
        // Use Application.persistentDataPath for a reliable save location across platforms
        string filePath = Path.Combine(Application.dataPath, fileName); 
        // Debug.Log("Persistant Path: " + Application.persistentDataPath);
        Debug.Log("Data Path: " + Application.dataPath);
        Debug.Log("File Path: " + filePath);
        // string filePath = Path.Combine(Application.dataPath, "capture.png");
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Saved screenshot to: " + filePath);
        
        // Clean up the screenshot texture
        Destroy(screenShot);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CaptureScreenshot();
        }
        
    }
}