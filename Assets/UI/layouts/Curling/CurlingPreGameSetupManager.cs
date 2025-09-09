using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CurlingPreGameSetupManager : MonoBehaviour
{
    public static CurlingPreGameSetupManager _instance;

    // public Sprite icon;
    [Header("Character Data (All)")]
    [SerializeField] public List<GameObject> listOfCharacters;

    [Header("Equipment Data (All)")]
    [SerializeField] public List<GameObject> listOfBrooms;
    [SerializeField] public List<GameObject> listOfStones;

    [Header("Team Selection")]
    [SerializeField] public GameObject selectedLeftSweeper;
    [SerializeField] public GameObject selectedRightSweeper;
    [SerializeField] public GameObject selectedThrower;

    [Header("Equipment Selection")]
    [SerializeField] public GameObject selectedLeftSweeperBroom;
    [SerializeField] public GameObject selectedRightSweeperBroom;
    [SerializeField] public GameObject selectedStone1;
    [SerializeField] public GameObject selectedStone2;
    [SerializeField] public GameObject selectedStone3;
    [SerializeField] public GameObject selectedStone4;
    [SerializeField] public GameObject selectedStone5;

    [Header("Rules")]
    [SerializeField] public GameObject curlingRules;

    // [Header("Settings")]
    // [SerializeField] public bool isTeamSelected;
    // [SerializeField] public bool isEquipmentSelected;

    public void Reset()
    {
        // Team
        selectedLeftSweeper = null;
        selectedRightSweeper = null;
        selectedThrower = null;

        // Equipment
        selectedLeftSweeperBroom = null;
        selectedRightSweeperBroom = null;
        selectedStone1 = null;
        selectedStone2 = null;
        selectedStone3 = null;
        selectedStone4 = null;
        selectedStone5 = null;

    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    public void Update()
    {

    }

    public void LoadData()
    {

    }

    public void OnSelectLeftSweeper(string characterId)
    {
        GameObject obj = listOfCharacters.Find(c => c.GetComponent<Character>().id == characterId);
        selectedLeftSweeper = obj;
    }

    public void OnSelectRightSweeper(string characterId)
    {
        GameObject obj = listOfCharacters.Find(c => c.GetComponent<Character>().id == characterId);
        selectedRightSweeper = obj;
    }

    public void OnSelectThrower(string characterId)
    {
        GameObject obj = listOfCharacters.Find(c => c.GetComponent<Character>().id == characterId);
        selectedThrower = obj;
    }
    
    



}








