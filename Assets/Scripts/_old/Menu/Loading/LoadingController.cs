// using System.Collections;
// using UnityEngine;
// using UnityEngine.AddressableAssets;
// using UnityEngine.ResourceManagement.AsyncOperations;

// public class LoadingController : UIController
// {
//     private void Update()
//     {
//         MainState currentState = MainManager._instance.currentState;

//         switch (currentState)
//         {
//             case MainState.Loading:
//                 // make things visible
//                 Show();
//                 break;
//             default:
//                 Hide();
//                 // hide this canvas
//                 break;
//         }
//     }
// }