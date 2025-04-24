// using UnityEngine;
// using UnityEngine.Rendering;
// using UnityEngine.Rendering.RenderGraphModule;
// using UnityEngine.Rendering.Universal;

// public class PixelationBlit : ScriptableRendererFeature
// {
//     private static Mesh fullscreenQuadMesh;

//     private static Mesh GetFullscreenQuadMesh()
//     {
//         if (fullscreenQuadMesh != null)
//             return fullscreenQuadMesh;

//         fullscreenQuadMesh = new Mesh
//         {
//             vertices = new Vector3[]
//             {
//                 new Vector3(-1, -1, 0),
//                 new Vector3(1, -1, 0),
//                 new Vector3(1, 1, 0),
//                 new Vector3(-1, 1, 0)
//             },
//             uv = new Vector2[]
//             {
//                 new Vector2(0, 0),
//                 new Vector2(1, 0),
//                 new Vector2(1, 1),
//                 new Vector2(0, 1)
//             },
//             triangles = new int[] { 0, 1, 2, 0, 2, 3 }
//         };
//         fullscreenQuadMesh.RecalculateNormals();
//         return fullscreenQuadMesh;
//     }

//     [System.Serializable]
//     public class PixelationSettings
//     {
//         public Material pixelationMaterial;
//         [Range(64, 1920)] public int pixelWidth = 320;
//         public bool lockAspectRatio = true;
//         public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing; // ⬅️ Corrected timing
//     }

//     public PixelationSettings settings = new PixelationSettings();
//     private PixelationPass pixelationPass;

//     public override void Create()
//     {
//         if (settings.pixelationMaterial != null)
//         {
//             pixelationPass = new PixelationPass(settings);
//         }
//     }

//     public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
//     {
//         if (pixelationPass != null)
//         {
//             renderer.EnqueuePass(pixelationPass);
//         }
//     }

//     class PixelationPass : ScriptableRenderPass
//     {
//         private readonly PixelationSettings settings;

//         public PixelationPass(PixelationSettings settings)
//         {
//             this.settings = settings;
//             renderPassEvent = settings.renderPassEvent;
//         }

//         public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
//         {
//             UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
//             var cameraData = frameData.Get<UniversalCameraData>();

//             // ✅ HARD SKIP for Scene View or any non-Game cameras:
//             if (cameraData.camera.cameraType != CameraType.Game)
//                 return;

//             if (cameraData.renderType != CameraRenderType.Base)
//                 return;

//             TextureHandle colorTarget = resourceData.activeColorTexture;

//             // ✅ Defensive check: If the color target isn't initialized, skip the pass:
//             if (!colorTarget.IsValid())
//                 return;

//             // ✅ Create the safe copy:
//             TextureDesc copyDesc = new TextureDesc(cameraData.cameraTargetDescriptor)
//             {
//                 colorFormat = cameraData.cameraTargetDescriptor.graphicsFormat,
//                 enableRandomWrite = true,
//                 name = "Pixelation Input Copy"
//             };
//             TextureHandle inputCopy = renderGraph.CreateTexture(copyDesc);

//             using (var builder = renderGraph.AddRasterRenderPass<PassData>("Pixelation Pass (Buffer Copy)", out var passData))
//             {
//                 builder.UseTexture(colorTarget, AccessFlags.ReadWrite);
//                 builder.UseTexture(inputCopy, AccessFlags.Write);

//                 passData.pixelationMaterial = settings.pixelationMaterial;
//                 passData.pixelWidth = settings.pixelWidth;
//                 passData.lockAspect = settings.lockAspectRatio;

//                 builder.SetRenderFunc((PassData data, RasterGraphContext ctx) =>
//                 {
//                     ctx.cmd.CopyTexture(colorTarget, inputCopy); // Safe copy!

//                     var descriptor = cameraData.cameraTargetDescriptor;
//                     float aspect = (float)descriptor.height / descriptor.width;
//                     Vector4 resolution = data.lockAspect
//                         ? new Vector4(data.pixelWidth, data.pixelWidth * aspect, 0, 0)
//                         : new Vector4(data.pixelWidth, data.pixelWidth, 0, 0);

//                     data.pixelationMaterial.SetVector("_PixelResolution", resolution);
//                     data.pixelationMaterial.SetTexture("_MainTex", inputCopy);

//                     ctx.cmd.DrawMesh(GetFullscreenQuadMesh(), Matrix4x4.identity, data.pixelationMaterial, 0, 0);
//                 });
//             }
//         }

//         private class PassData
//         {
//             public Material pixelationMaterial;
//             public int pixelWidth;
//             public bool lockAspect;
//         }
//     }
// }
