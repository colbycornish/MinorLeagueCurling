// using UnityEngine;
// using UnityEngine.Rendering;
// using UnityEngine.Rendering.RenderGraphModule;
// using UnityEngine.Rendering.Universal;

// public class PixelationRendererFeature : ScriptableRendererFeature
// {
//     [System.Serializable]
//     public class PixelationSettings
//     {
//         public Material pixelationMaterial;
//         [Range(64, 1920)] public int pixelWidth = 320;
//         public bool lockAspectRatio = true;
//         public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
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

//             // Skip Scene View and other non-Game cameras
//             if (cameraData.camera.cameraType != CameraType.Game)
//                 return;

//             if (cameraData.renderType != CameraRenderType.Base)
//                 return;

//             TextureHandle inputTexture = resourceData.activeColorTexture;

//             // Create the output target for the pixelation result
//             TextureDesc outputDesc = new TextureDesc(cameraData.cameraTargetDescriptor)
//             {
//                 colorFormat = cameraData.cameraTargetDescriptor.graphicsFormat,
//                 enableRandomWrite = true,
//                 name = "Pixelation Output"
//             };
//             TextureHandle outputTarget = renderGraph.CreateTexture(outputDesc);

//             using (var builder = renderGraph.AddRasterRenderPass<PassData>("Pixelation Pass", out var passData))
//             {
//                 builder.UseTexture(inputTexture, AccessFlags.Read);
//                 builder.UseTexture(outputTarget, AccessFlags.Write);

//                 builder.SetRenderAttachment(outputTarget, 0);

//                 passData.pixelationMaterial = settings.pixelationMaterial;
//                 passData.pixelWidth = settings.pixelWidth;
//                 passData.lockAspect = settings.lockAspectRatio;

//                 builder.SetRenderFunc((PassData data, RasterGraphContext ctx) =>
//                 {
//                     var descriptor = cameraData.cameraTargetDescriptor;
//                     float aspect = (float)descriptor.height / descriptor.width;
//                     Vector4 resolution = data.lockAspect
//                         ? new Vector4(data.pixelWidth, data.pixelWidth * aspect, 0, 0)
//                         : new Vector4(data.pixelWidth, data.pixelWidth, 0, 0);

//                     data.pixelationMaterial.SetVector("_PixelResolution", resolution);
//                     data.pixelationMaterial.SetTexture("_MainTex", inputTexture);

//                     // Blitter.BlitTexture(ctx.cmd, inputTexture, outputTarget, data.pixelationMaterial, 0);
//                     // ctx.cmd.DrawMesh(fullscreenQuad, Matrix4x4.identity, material, 0, 0);
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