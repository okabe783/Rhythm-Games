using UnityEngine;

/// <summary>選択されたデータを保存する</summary>
[CreateAssetMenu(fileName = "SelectedCharacterData", menuName = "Game/Selected Data", order = 1)]
public class SelectData : ScriptableObject
{
    public SoundTable ItemData { get; private set; } //SoundDataを保存
    
    public int _characterId { get; set; } //CharacterのIdを保存

    public void SetItemData(SoundTable soundTable)
    {
        ItemData = soundTable;
    }
}
