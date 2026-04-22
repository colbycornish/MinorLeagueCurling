using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Playables;
using Unity.Cinemachine;
using CharacterNPC.v2;
using UnityEngine.AI;
using CharacterNPCJobs;

namespace CurlingObjects
{
    public class AnnouncerBooth : MonoBehaviour
    {
        public string boothID;
        public string boothName;
        public GameObject boothContainerObject;
        public Transform spawnLocationBroomy;
        public Transform spawnLocationMick;
        public Transform lookAtTargetBroomy;
        public Transform lookAtTargetMick;
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

        public void Setup()
        {
            LoadAnnouncers();
            mickProfileCamera.Follow = mickTheMooseSliderson.transform;
            mickProfileCamera.LookAt = mickTheMooseSliderson.transform;

            broomyProfileCamera.Follow = broomyOChallahan.transform;
            broomyProfileCamera.LookAt = broomyOChallahan.transform;

            dollyTwoShotCamera.Follow = gameObject.transform;
            dollyTwoShotCamera.LookAt = gameObject.transform;
        }

        public void LoadAnnouncers()
        {
            mickTheMooseSliderson = Instantiate(
                mickTheMooseSliderson, 
                spawnLocationMick.transform.position, 
                Quaternion.identity,
                boothContainerObject.transform
            );

            

            broomyOChallahan = Instantiate(
                broomyOChallahan, 
                spawnLocationBroomy.transform.position, 
                Quaternion.identity,
                boothContainerObject.transform
            );

            if (lookAtTargetBroomy != null)
            {
                broomyOChallahan.transform.LookAt(lookAtTargetBroomy);
            }

            if (lookAtTargetMick != null)
            {
                mickTheMooseSliderson.transform.LookAt(lookAtTargetMick);
            }

            

            ConfigureAnnouncer(announcer: mickTheMooseSliderson);
            ConfigureAnnouncer(announcer: broomyOChallahan);
        }

        private void ConfigureAnnouncer(GameObject announcer)
        {
            CharacterNPC.v2.Character c = announcer.GetComponent<CharacterNPC.v2.Character>();
            NavMeshAgent navAgent = announcer.GetComponent<NavMeshAgent>();

            c.Parameters.Posture.DesiredPosture = CharacterPostureState.Sitting;
            navAgent.baseOffset = 0.22f; // Adjust as needed to ensure the character is properly seated on the chair

            c.Parameters.Status.IsCurling = false;
            c.Parameters.Jobs.CurrentJob = JobStateType.Idle;
            c.Parameters.Jobs.AvailableJobStates.Add(JobStateType.Announcer);
            c.Parameters.Jobs.DesiredJob = JobStateType.Announcer;
            c.Parameters.Jobs.AvailableActions.Add(ActionType.Talk);
            c.Parameters.Jobs.DesiredAction = ActionType.Talk;
            c.Parameters.Dialogue.IsEngagedInDialogueWithOtherCharacters = true; // Set to true to indicate the announcer is engaged in dialogue with the player



        }
        
        public void UnloadAnnouncers()
        {
            Destroy(mickTheMooseSliderson);
            Destroy(broomyOChallahan);
            // Consider pooling announcer objects for performance if they will be reused frequently
            // For now, we simply destroy them when unloading
            // In the future, you could implement an object pool to reuse announcer instances instead of destroying and instantiating them repeatedly
            
        }
    }
}
