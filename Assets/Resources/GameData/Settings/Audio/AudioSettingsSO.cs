using UnityEngine;
using Unity.Properties;

[CreateAssetMenu(fileName = "AudioSettingsSO", menuName = "Scriptable Objects/AudioSettingsSO")]
public class AudioSettingsSO : ScriptableObject
{
    [SerializeField, DontCreateProperty] 
    private float _BackgroundMusicVolume;

    [CreateProperty] 
    public float BackgroundMusicVolume => _BackgroundMusicVolume;

    [SerializeField, DontCreateProperty] 
    private float _SFXVolume;
    
    [CreateProperty] 
    public float SFXVolume => _SFXVolume;

    [SerializeField, DontCreateProperty] 
    private float _UIVolume;
    
    [CreateProperty] 
    public float UIVolume => _UIVolume;

    [SerializeField, DontCreateProperty] 
    private float _DialogueVolume;
    
    [CreateProperty] 
    public float DialogueVolume => _DialogueVolume;
    
}
