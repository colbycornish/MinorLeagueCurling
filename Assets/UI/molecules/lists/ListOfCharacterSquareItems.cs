using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class ListOfCharacterSquareItems : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] private GameObject listArea;
    [SerializeField] private GameObject listItemPrefab;
    [SerializeField][HideInInspector] private int selectedIndex = 0;
    [SerializeField][HideInInspector] private int itemCount = 0;

    public void Init()
    {

    }

    public void UpdateUI()
    {

    }
    
    public void CreateList()
    {

    }

    public void HighlightNext()
    {
        if (selectedIndex < itemCount)
        {
            selectedIndex++;
            UpdateSelectionDisplay();
        }
    }

    public void HighlightPrev()
    {
        if (selectedIndex > 0){
            selectedIndex--;
            UpdateSelectionDisplay();
        }
    }

    public void UpdateSelectionDisplay()
    {
        
    }

    public void BuildList(
        GameObject[] characterPrefabs
    )
    {
        // foreach (Transform child in listArea.transform) {
        //     GameObject.Destroy(child.gameObject);
        // }

        // foreach (GameObject characterPrefab in characterPrefabs) {
        //     GameObject listItem = Instantiate(listItemPrefab, listArea.transform);
        //     CharacterSquareItemController controller = listItem.GetComponent<CharacterSquareItemController>();
        //     controller.Init(characterPrefab);
        //     controller.UpdateUI();
        // }
    }


}
