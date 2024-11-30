using UnityEngine;

/// <summary>使用するサウンドとシーンを保存</summary>
[CreateAssetMenu(fileName = "SoundTable", menuName = "Sound/SoundTable")]
public class SoundTable : ScriptableObject
{
    public int TableId;
    public int PreviewTime;
    public string SoundName;
    public string SelectScene;
}