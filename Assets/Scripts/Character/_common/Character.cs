using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string id;
    public string version;
    public string firstName;
    public string lastName;
    public string fullName => $"{firstName} {lastName}";
    public string description;
    public string iconPath; // Path to the icon asset
    public string addressableModelPath; // Path to the model asset
    public CurlingPlayer curlingPlayerData;
    public GameObject model;

    public void Start()
    {
        BuildRandomCurlingPlayerData();
        // AttachModel();
    }

    public void UseInCanvasDisplayMode(){
        if (model != null){
            model.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        }
    }

    public void UseInGameWorld(){
        if (model != null){
            model.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        }
    }

    public void BuildRandomCurlingPlayerData()
    {
        curlingPlayerData = new CurlingPlayer();
        curlingPlayerData.name = fullName;
        curlingPlayerData.characterId = id;
        curlingPlayerData.BuildRandomStats();
    }

    private void ErrorCheck(){
        if (model == null){
            Debug.LogError($"Character {fullName} is missing a model.");
        }
        
    }

    private void AttachModel(){
        if (model == null){
            Debug.Log($"Attaching model for character {fullName} unsafely. Should fix this...");
            foreach (Transform child in this.transform) {
                if (child.name != "Brain"){
                    model = child.gameObject;
                }
            }
            
        }
    }

}

