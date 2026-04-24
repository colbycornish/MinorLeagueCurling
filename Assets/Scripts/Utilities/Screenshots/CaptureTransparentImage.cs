using UnityEditor;
using UnityEngine;
using System.IO;

public class CaptureTransparentImage : MonoBehaviour
{
    public Camera captureCamera; // Drag your dedicated camera here in the Inspector
    public int captureWidth = 1024;
    public int captureHeight = 1024;
    public string saveDirectory = "UI/Textures/Avatars/ScreenCaptures/";

    public void CaptureAndSaveScreenshot()
    {

        Texture2D screenShot = TakeScreenshot();
        SaveCaptureToPNGFile(
            screenShot: screenShot, 
            fileName: null //"CharacterScreenshot"
        );        
    }

    public Texture2D TakeScreenshot()
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

        return screenShot;
    }

    public static Sprite TextureToSprite(Texture2D texture)
    {
        Sprite newSprite = Sprite.Create(
            texture, 
            new Rect(0f, 0f, texture.width, texture.height), 
            new Vector2(0.5f, 0.5f), 100f
        );
        
        return newSprite;
    }

    // void OnPreprocessTexture()
    // {
    //     // Check if the file is a PNG
    //     // if (assetPath.Contains("/Sprites/")) // Optional: Limit to a specific folder
    //     // {
    //     TextureImporter importer = assetImporter as TextureImporter;
    //     if (importer != null)
    //     {
    //         importer.textureType = TextureImporterType.Sprite;
    //         importer.spriteMode = (int)SpriteImportMode.Single; // Set to 0 (Single) or 1 (Multiple)
    //         importer.alphaIsTransparency = true;
    //         // Optional: apply automatically
    //         // AssetDatabase.ImportAsset(assetPath);
    //     }
    //     // }
    // }

    public void SaveCaptureToPNGFile(
        Texture2D screenShot, 
        string fileName
    )
    {
        if (fileName == null || fileName == "")
        {
            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            fileName = "screenshot_" + timestamp + ".png";
        }

        if (fileName.EndsWith(".png") == false)
        {
            fileName += ".png";
        }

        byte[] bytes = screenShot.EncodeToPNG();
        

        string subDirectory = saveDirectory;
        string fullFileName = subDirectory + fileName;
        string filePath = Path.Combine(Application.dataPath, fullFileName); 
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Saved screenshot to: " + filePath);
        
        // Clean up the screenshot texture
        Destroy(screenShot);
    }

    
}






// public void CaptureScreenshot()
//     {

//         Texture2D screenShot = TakeScreenshot();
//         SaveCaptureToFile(
//             screenShot: screenShot, 
//             fileName: "CharacterScreenshot"
//         );
        // // Create a new render texture
        // RenderTexture rt = new RenderTexture(captureWidth, captureHeight, 24, RenderTextureFormat.ARGB32);
        // captureCamera.targetTexture = rt;
        
        // // Render the camera view to the render texture
        // captureCamera.Render();

        // // Set the active render texture to the new one for reading pixels
        // RenderTexture.active = rt;
        
        // // Create a new Texture2D with ARGB32 format to support transparency
        // Texture2D screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.ARGB32, false);
        // screenShot.ReadPixels(new Rect(0, 0, captureWidth, captureHeight), 0, 0);
        // screenShot.Apply();

        // // Clean up
        // captureCamera.targetTexture = null;
        // RenderTexture.active = null;
        // Destroy(rt);

        

        // Encode to PNG and save to file
        // byte[] bytes = screenShot.EncodeToPNG();
        // string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        // string fileName = "screenshot_" + timestamp + ".png";

        // string subDirectory = "UI/Textures/Avatars/ScreenCaptures/";
        // string fullFileName = subDirectory + fileName;
        // string filePath = Path.Combine(Application.dataPath, fullFileName); 
        // File.WriteAllBytes(filePath, bytes);
        // Debug.Log("Saved screenshot to: " + filePath);
        
        // // Clean up the screenshot texture
        // Destroy(screenShot);
    // }