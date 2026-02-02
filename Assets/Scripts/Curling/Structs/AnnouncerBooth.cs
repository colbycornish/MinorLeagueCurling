using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Playables;
using Unity.Cinemachine;

namespace CurlingObjects
{
    public class AnnouncerBooth : MonoBehaviour
    {
        public string boothID;
        public string boothName;
        public GameObject boothContainerObject;
        public Transform spawnLocation1;
        public Transform spawnLocation2;
        public List<GameObject> lights;

        [Header("Cameras")]
        public List<GameObject> cameras;
        public CinemachineCamera dollyTwoShotCamera;
        /// <summary>
        /// Micks profile camera. Left.
        /// </summary>
        public CinemachineCamera mickProfileCamera;
        /// <summary>
        /// Broomy's profile camera. Right.
        /// </summary>
        public CinemachineCamera broomyProfileCamera;

        [Header("Announcer Prefabs")]
        public GameObject mickTheMooseSliderson;
        public GameObject broomyOChallahan;

        [Header("Cinematics")]
        public PlayableDirector announcerCommentaryTimeline;

        public AnnouncerBooth(
            int id,
            string name,
            string location
        )
        {
            // boothID = id;
            // announcerName = name;
            // boothLocation = location;
        }

        public void LoadAnnouncers()
        {
            mickTheMooseSliderson = Instantiate(
                mickTheMooseSliderson, 
                spawnLocation1.transform.position, 
                Quaternion.identity
            );

            broomyOChallahan = Instantiate(
                broomyOChallahan, 
                spawnLocation2.transform.position, 
                Quaternion.identity
            );
            //load announcers into booth
        }
        
        public void UnloadAnnouncers()
        {
            //unload announcers from booth
        }
    }
}
