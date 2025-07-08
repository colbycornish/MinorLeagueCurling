using UnityEngine;
using System.Collections.Generic;

public enum AudioType
{
    None,
    Music,
    FXEffect,
    Dialogue,
    UI
}


public class AudioItem : MonoBehaviour
{
    public AudioType audioType = AudioType.None;
    public string audioName = "Audio Name";
    public AudioClip audioClip;
    public bool isLooping = false;
    public float volume = 1.0f;
    public float pitch = 1.0f;
    public bool isMuted = false;
    // Additional properties can be added as needed
}
