using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MusicViewer : MonoBehaviour
{
    [SerializeField] private MusicScrollView _musicScrollView;
    [SerializeField] private SoundTableList _soundTableList; //曲データのリスト
    [SerializeField] private Text selectedItemInfo;

    private void Start()
    {
        _musicScrollView.OnSelectionChanged(OnSelectionChanged);
        //データリストに保存されているデータを読み込む
        var items = Enumerable.Range(0, _soundTableList.SoundTables.Count).Select(i =>
            new MusicItemData(_soundTableList.SoundTables[i])).ToArray();
        
        _musicScrollView.UpdateData(items);
        _musicScrollView.SelectCell(0);
    }
    
    //選択されたセルに保存してあるシーンの遷移先を表示
    private void OnSelectionChanged(MusicItemData itemData)
    {
        selectedItemInfo.text = $"{itemData.SelectScene}";
    }
}
