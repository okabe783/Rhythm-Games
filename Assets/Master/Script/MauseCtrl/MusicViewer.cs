using System.Linq;
using UnityEngine;

public class MusicViewer : MonoBehaviour
{
    [SerializeField] private MusicScrollView _musicScrollView;
    [SerializeField] private SoundTableList _soundTableList; //曲データのリスト

    private void Start()
    {
        //ToDo:スクリプタブルオブジェクトのデータを取り込む
        //Uiを表示してデータリストに保存されているデータを読み込む
        var items = Enumerable.Range(0, _soundTableList.SoundTables.Count).Select(i =>
            new MusicItemData(i, $"music{i:D2}")).ToArray();
        
        _musicScrollView.UpdateData(items);
    }
}
