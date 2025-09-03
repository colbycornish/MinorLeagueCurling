using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasDisplayAreaController : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] public int spacingX;
    [SerializeField] public List<CanvasDisplaySingleCharacterController> items;


    public void Init()
    {

    }

    public void UpdateUI()
    {

    }

    public void AddItem(CanvasDisplaySingleCharacterController item)
    {
        // GameObject lastItem = items[items.Count - 1];
        items.Add(item);
    }

    public void RemoveItem(CanvasDisplaySingleCharacterController item)
    {
        items.Remove(item);
    }
    
    public void Clear()
    {
        items = new List<CanvasDisplaySingleCharacterController>();
    }


    

}
