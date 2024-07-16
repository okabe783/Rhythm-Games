using UnityEngine;

/// <summary>選択されたデータを保存する</summary>
[CreateAssetMenu(fileName = "SelectedCharacterData", menuName = "Game/Selected Data", order = 1)]
public class SelectData : ScriptableObject
{
    public MusicItemData ItemData { get; set; } //SoundDataを保存
    public CharacterDataBase CharacterData { get; set; }　//CharacterDataを保存
}