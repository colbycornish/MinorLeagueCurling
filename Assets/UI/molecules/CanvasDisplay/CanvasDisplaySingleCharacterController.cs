using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class CanvasDisplaySingleCharacterController : MonoBehaviour
{
    // public Sprite icon;
    [Header("Components")]
    [SerializeField] public string id;
    [SerializeField] public GameObject characterPrefab;
    [SerializeField] public GameObject model;
    [SerializeField] public GameObject modelArea;

    [Header("Cameras and Render Textures")]
    [SerializeField] public Camera cameraFace;
    [SerializeField] public Camera cameraFull;
    [SerializeField][HideInInspector] public RenderTexture rtFace;
    [SerializeField][HideInInspector] public RenderTexture rtFull;

    public void Init(
        GameObject characterPrefabInstance,
        string characterId
    )
    {
        id = characterId;
        characterPrefab = characterPrefabInstance;
        SetupModel(characterPrefab);
        SetupRenderTextures();
        AdjustFaceCamera();
    }

    public void AdjustFaceCamera()
    {
        Transform modelHeadTransform = transform.Find("Head_M");
        
        if (modelHeadTransform != null)
        {
            // transformY
            cameraFace.transform.position = modelHeadTransform.position + new Vector3(0, 0, -1.71f);
            cameraFace.transform.LookAt(modelHeadTransform.position);
        }
    }

    public void UpdateUI()
    {

    }

    public void SetupModel(GameObject characterPrefabInstance)
    {
        if (model != null)
        {
            Destroy(model);
        }

        characterPrefabInstance.SetLayerRecursively("UI-ObjectRenderer");
        model = Instantiate(characterPrefabInstance, modelArea.transform);
        model.transform.localPosition = Vector3.zero;
        model.SetLayerRecursively("UI-ObjectRenderer");
        // model.transform.localRotation = Quaternion.Euler(0, 180, 0);
        // model.transform.localScale = new Vector3(100, 100, 100);

    }

    public void SetupRenderTextures()
    {
        rtFace = new RenderTexture(256, 256, 24);
        cameraFace.targetTexture = rtFace;

        rtFull = new RenderTexture(1024, 1024, 24);
        cameraFull.targetTexture = rtFull;
    }

    public RenderTexture GetRenderTextureFace()
    {
        return cameraFace.targetTexture;
    }

    public RenderTexture GetRenderTextureFull()
    {
        return cameraFull.targetTexture;
    }
    

}


public static class GameObjectExtensions
{
    /// <summary>
    /// Sets the layer of the GameObject and all its children recursively.
    /// </summary>
    /// <param name="obj">The root GameObject.</param>
    /// <param name="newLayer">The new layer to assign (integer value).</param>
    public static void SetLayerRecursively(this GameObject obj, int newLayer)
    {
        if (obj == null)
        {
            return;
        }

        obj.layer = newLayer; // Set the layer of the current GameObject

        // Iterate through all children and recursively call the method
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    /// <summary>
    /// Sets the layer of the GameObject and all its children recursively using a layer name.
    /// </summary>
    /// <param name="obj">The root GameObject.</param>
    /// <param name="layerName">The name of the new layer.</param>
    public static void SetLayerRecursively(this GameObject obj, string layerName)
    {
        int newLayer = LayerMask.NameToLayer(layerName);
        SetLayerRecursively(obj, newLayer);
    }
}