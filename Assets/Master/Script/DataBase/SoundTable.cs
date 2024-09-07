using UnityEngine;

[CreateAssetMenu(fileName = "SoundTable", menuName = "Sound/SoundTable")]
//使用するサウンドとシーンを保存
public class SoundTable : ScriptableObject
{
    public int TableId;
    public int PreviewTime;
    public string SoundName;
    public string SelectScene;
}