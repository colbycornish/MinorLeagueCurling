using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CharacterScreenshotArea : MonoBehaviour
{
    public Camera captureCamera; 
    private CaptureTransparentImage imageCapture;
    public GameObject SubjectsArea;
    
    private List<GameObject> subjectObjects = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageCapture = captureCamera.GetComponent<CaptureTransparentImage>();
        GetChildren();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCaptureProcess();
        }
        
    }

    void StartCaptureProcess()
    {
        StartCoroutine(CaptureImagesForAllSubjects());
    }


    void GetChildren()
    {
        for (int i = 0; i < SubjectsArea.transform.childCount; i++)
        {
            GameObject child = SubjectsArea.transform.GetChild(i).gameObject;
            child.SetActive(false);
            subjectObjects.Add(child);
        }
    }
    
    private IEnumerator CaptureImagesForAllSubjects()
    {
        foreach (GameObject subject in subjectObjects)
        {
            Debug.Log("Processing subject: " + subject.name);
            if (subject.GetComponent<CharacterNPC.v2.Character>() != null)
            {
                Debug.Log("....Character Found!");
                subject.SetActive(true);
                yield return Wait();
                // yield return new WaitForEndOfFrame();
                CaptureImageForSingleSubject(subject);
                subject.SetActive(false);
                Debug.Log("....next....");
                // yield return new WaitForEndOfFrame();
                
            }
        }
    } 

    IEnumerator Wait() {
        yield return new WaitForSeconds(2);
    }

    private void CaptureImageForSingleSubject(GameObject subject)
    {
        
        string fileName = subject.name.ToString();
        Debug.Log("Capturing image for: " + fileName);

        Texture2D screenShot = imageCapture.TakeScreenshot();
        imageCapture.SaveCaptureToPNGFile(
            screenShot: screenShot, 
            fileName: fileName
        );  

        // imageCapture.CaptureAndSaveScreenshot();
        subject.SetActive(false);
    }

    
    // void TurnChildOff(Gam
}
