using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace CurlingObjects
{
    public class AnnouncerBooth : MonoBehaviour
    {
        public string boothID;
        public string boothName;
        public string spawnLocation1;
        public string spawnLocation2;
        public GameObject lights;

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
    }
}
