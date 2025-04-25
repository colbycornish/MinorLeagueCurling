
// using UnityEngine;
// using UnityEngine.Rendering;
// using UnityEngine.Rendering.Universal;

// public class PixelationBlit : ScriptableRendererFeature
// {
//     [System.Serializable]
//     public class PixelationSettings
//     {
//         public Material pixelationMaterial;
//         [Range(64, 1920)] public int pixelWidth = 320;
//         public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRendering;
//     }



//     public PixelationSettings settings = new PixelationSettings();

//     class PixelationPass : ScriptableRenderPass
//     {
//         private Material pixelationMaterial;
//         private int pixelWidth;

//         private RenderTargetIdentifier source;
// //       private RenderTargetIdentifier cameraColorTarget;

//         public PixelationPass(Material material, int width)
//         {
//             pixelationMaterial = material;
//             pixelWidth = width;
//         }

//         public void Setup(RenderTargetIdentifier src)
//         {
//             source = src;
//         }


        




// //         public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
// //         {
// //             ConfigureInput(ScriptableRenderPassInput.Color);
// //         }


//         public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
//         {
//             if (pixelationMaterial == null)
//                 return;

//             CommandBuffer cmd = CommandBufferPool.Get("Pixelation Pass");

//             RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
//             float aspect = (float)descriptor.height / descriptor.width;
//             Vector4 resolution = new Vector4(pixelWidth, pixelWidth * aspect, 0, 0);
//             pixelationMaterial.SetVector("_PixelResolution", resolution);

//             int tempID = Shader.PropertyToID("_TempPixelationTexture");
//             cmd.GetTemporaryRT(tempID, descriptor);
//             cmd.Blit(source, tempID, pixelationMaterial);
//             cmd.Blit(tempID, source);
//             cmd.ReleaseTemporaryRT(tempID);

//             context.ExecuteCommandBuffer(cmd);
//             CommandBufferPool.Release(cmd);
//         }

        

// //         public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
// //         {
// //             if (pixelationMaterial == null)
// //                 return;

// //             CommandBuffer cmd = CommandBufferPool.Get("Pixelation Pass");

// //             RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
// //             float aspect = (float)descriptor.height / descriptor.width;
// //             Vector4 resolution = new Vector4(pixelWidth, pixelWidth * aspect, 0, 0);
// //             pixelationMaterial.SetVector("_PixelResolution", resolution);

// //             cameraColorTarget = renderingData.cameraData.renderer.GetCameraColorBackBuffer(cmd);
// //             int tempID = Shader.PropertyToID("_TempPixelationTexture");
// //             cmd.GetTemporaryRT(tempID, descriptor);
// //             Blit(cmd, cameraColorTarget, tempID, pixelationMaterial);
// //             Blit(cmd, tempID, cameraColorTarget);
// //             cmd.ReleaseTemporaryRT(tempID);

// //             context.ExecuteCommandBuffer(cmd);
// //             CommandBufferPool.Release(cmd);
// //         }
//     }

//     private PixelationPass pixelationPass;

//     //     private PixelationPass pixelationPass;

//     public override void Create()
//     {
//         if (settings.pixelationMaterial != null)
//         {
//             pixelationPass = new PixelationPass(settings.pixelationMaterial, settings.pixelWidth)
//             {
//                 renderPassEvent = settings.renderPassEvent
//             };
//         }
//     }


// //     public override void Create()
// //     {
// //         if (settings.pixelationMaterial != null)
// //         {
// //             pixelationPass = new PixelationPass(settings.pixelationMaterial, settings.pixelWidth)
// //             {
// //                 renderPassEvent = settings.renderPassEvent
// //             };
// //         }
// //     }

//     // public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
//     // {
//     //     if (pixelationPass != null)
//     //     {
//     //         var cameraTarget = renderingData.cameraData.renderer.cameraColorTargetHandle;
//     //         pixelationPass.Setup(cameraTarget);
//     //         renderer.EnqueuePass(pixelationPass);
//     //     }
//     // }


//     public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
//     {
//         if (pixelationPass != null)
//         {
//             renderer.EnqueuePass(pixelationPass);
//         }
//     }
// }




// // using UnityEngine;
// // using UnityEngine.Rendering;
// // using UnityEngine.Rendering.Universal;

// // public class PixelationBlit : ScriptableRendererFeature
// // {
// //     [System.Serializable]
// //     public class PixelationSettings
// //     {
// //         public Material pixelationMaterial;
// //         [Range(64, 1920)] public int pixelWidth = 320;
// //         public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRendering;
// //     }

// //     public PixelationSettings settings = new PixelationSettings();

// //     class PixelationPass : ScriptableRenderPass
// //     {
// //         
// //     }




// // }
