using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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
        public List<GameObject> cameras;

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
            //load announcers into booth
        }
        
        public void UnloadAnnouncers()
        {
            //unload announcers from booth
        }
    }
}
