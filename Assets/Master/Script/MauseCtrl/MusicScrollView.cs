using UnityEngine;
using FancyScrollView;
using System.Collections.Generic;

/// <summary>音楽データ</summary>
public class MusicItemData
{
    public int _musicId;
    public readonly string _musicName;

    public MusicItemData(int id, string musicName)
    {
        _musicId = id;
        _musicName = musicName;
    }
}

internal class MusicScrollView : FancyScrollView<MusicItemData>
{
    [SerializeField] private Scroller _scroller;
    [SerializeField] private GameObject _cellPrefab;
    
    protected override GameObject CellPrefab => _cellPrefab;

    //スクロールの位置が変わるたびにUpdatePositionが呼ばれるように設定
    protected override void Initialize()
    {
        _scroller.OnValueChanged(UpdatePosition);
    }

    //ScrollViewの内容を更新
    public void UpdateData(IList<MusicItemData> items)
    {
        UpdateContents(items);
        _scroller.SetTotalCount(items.Count);
    }
}