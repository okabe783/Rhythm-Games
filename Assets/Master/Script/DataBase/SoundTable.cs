using UnityEngine;

[CreateAssetMenu(fileName = "SoundTable", menuName = "Sound/SoundTable")]
//使用するサウンドとシーンを保存
public class SoundTable : ScriptableObject
{
    [field: SerializeField] public int TableId { get; private set; }
    [field: SerializeField] public int PreviewTime { get; private set;}
    [field: SerializeField] public string SoundName { get; private set;}
    [field: SerializeField] public string SelectScene { get; private set;}
}