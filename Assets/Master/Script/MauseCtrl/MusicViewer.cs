using System.Linq;
using UnityEngine;

public class MusicViewer : MonoBehaviour
{
    [SerializeField] private MusicScrollView _musicScrollView;
    [SerializeField] private SoundTableList _soundTableList; //曲データのリスト

    private void Start()
    {
        //データリストに保存されているデータを読み込む
        var items = Enumerable.Range(0, _soundTableList.SoundTables.Count).Select(i =>
            new MusicItemData(_soundTableList.SoundTables[i])).ToArray();
        
        _musicScrollView.UpdateData(items);
    }
}
