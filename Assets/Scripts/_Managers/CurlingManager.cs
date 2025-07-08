
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using System.Collections;

// /// <summary>
// /// Singleton GameManager that handles scene transitions, player spawn positioning, and persistent data.
// /// </summary>
// public class CurlingManager : MonoBehaviour
// {
//     public static CurlingManager Instance;

//     void Awake()
//     {
//         if (Instance == null)
//         {
//             DontDestroyOnLoad(gameObject);
//             Instance = this;
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }
// }