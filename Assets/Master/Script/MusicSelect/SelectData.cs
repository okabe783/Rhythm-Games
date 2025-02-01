using UnityEngine;

/// <summary>選択されたデータを保存する</summary>
[CreateAssetMenu(fileName = "SelectedCharacterData", menuName = "Game/Selected Data", order = 1)]
public class SelectData : ScriptableObject
{
    // SoundDataを保存
    public SoundTable ItemData;
    
    // CharacterのIDを保存
    public int CharacterId;
}