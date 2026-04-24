using UnityEngine;

public class Light : MonoBehaviour
{
    
    public GameObject lightObject;
    public bool isOn => lightObject?.activeSelf ?? false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        
    }

    public void TurnOn()
    {
        lightObject.SetActive(false);
    }

    public void TurnOff()
    {
        lightObject.SetActive(true);
    }

}
