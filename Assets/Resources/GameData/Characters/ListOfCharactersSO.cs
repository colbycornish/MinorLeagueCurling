using UnityEngine;
using System.Collections.Generic;
using Unity.Properties;


[CreateAssetMenu(fileName = "ListOfCharacters", menuName = "Scriptable Objects/ListOfCharacters")]
public class ListOfCharactersSO : ScriptableObject
{
    [SerializeField, DontCreateProperty]
    private List<CharacterSO> _ListOfCharacters = new List<CharacterSO>();

    [CreateProperty]
    public List<CharacterSO> ListOfCharacters => _ListOfCharacters;

}
