using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class CanvasDisplaySingleBroomController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] public string id;
    [SerializeField] public GameObject broomPrefab;
    [SerializeField] public GameObject model;
    [SerializeField] public GameObject modelArea;

    [Header("Cameras and Render Textures")]
    [SerializeField] public Camera cameraHead;
    [SerializeField] public Camera cameraFull;
    [SerializeField][HideInInspector] public RenderTexture rtHead;
    [SerializeField][HideInInspector] public RenderTexture rtFull;

    public void Init(
        GameObject broomPrefabInstance,
        string broomId
    )
    {
        id = broomId;
        broomPrefab = broomPrefabInstance;
        SetupModel(broomPrefab);
        SetupRenderTextures();
        AdjustFaceCamera();
    }

    public void AdjustFaceCamera()
    {
        Transform modelHeadTransform = transform.Find("Brush");
        
        if (modelHeadTransform != null)
        {
            cameraHead.transform.position = modelHeadTransform.position + new Vector3(0, 0, -1.71f);
            cameraHead.transform.LookAt(modelHeadTransform.position);
        }
    }

    public void UpdateUI()
    {

    }

    public void SetupModel(GameObject broomPrefabInstance)
    {
        if (model != null)
        {
            Destroy(model);
        }

        broomPrefabInstance.SetLayerRecursively("UI-ObjectRenderer");
        model = Instantiate(broomPrefabInstance, modelArea.transform);
        model.transform.localPosition = Vector3.zero;
        model.SetLayerRecursively("UI-ObjectRenderer");
    }

    public void SetupRenderTextures()
    {
        rtHead = new RenderTexture(256, 256, 24);
        cameraHead.targetTexture = rtHead;

        rtFull = new RenderTexture(1024, 1024, 24);
        cameraFull.targetTexture = rtFull;
    }

    public RenderTexture GetRenderTextureHead()
    {
        return cameraHead.targetTexture;
    }

    public RenderTexture GetRenderTextureFull()
    {
        return cameraFull.targetTexture;
    }

}

