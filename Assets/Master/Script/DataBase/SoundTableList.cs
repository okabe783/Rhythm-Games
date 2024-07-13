using System.Collections.Generic;
using UnityEngine;

/// <summary>SoundTableをリストにまとめる</summary>
[CreateAssetMenu(fileName = "SoundTableList", menuName = "Sound/SoundTableList")]
public class SoundTableList : ScriptableObject
{
    [SerializeField] private List<SoundTable> _soundTables = new();

    public List<SoundTable> SoundTables => _soundTables;
}