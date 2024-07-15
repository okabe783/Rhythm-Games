using System.Collections.Generic;
using UnityEngine;

/// <summary> CharacterDataBaseをリストにまとめる </summary>
[CreateAssetMenu(fileName = "CharacterDataBaseList", menuName = "Scriptable/CharacterDataBaseList")]
public class CharacterDataBaseList : ScriptableObject
{
    [SerializeField] private List<CharacterDataBase> _characterDataBase = new();

    public List<CharacterDataBase> CharacterDataBase => _characterDataBase;
}