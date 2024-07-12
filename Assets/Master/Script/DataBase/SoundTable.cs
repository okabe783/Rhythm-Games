using UnityEngine;

[CreateAssetMenu(fileName = "SoundTable", menuName = "Sound/SoundTable")]
//使用するサウンドとシーンを保存
public class SoundTable : ScriptableObject
{
    [SerializeField] private int _tableId;　//格納されているid
    [SerializeField] private int _previewTime; //曲選択時に流す曲の時間
    [SerializeField] private string _soundName; //選択された曲
    [SerializeField] private string _selectScene;　//選択された曲のノーツが流れるシーン
}