using UnityEngine;
using FancyScrollView;
using System.Collections.Generic;

/// <summary>保存された音楽データ</summary>
public class MusicItemData
{
    public int TableId { get; }
    public int PreviewTime { get; }
    public string SoundName { get; }
    public string SelectScene { get; }

    public MusicItemData(SoundTable soundTable)
    {
        TableId = soundTable.TableId;
        PreviewTime = soundTable.PreviewTime;
        SoundName = soundTable.SoundName;
        SelectScene = soundTable.SelectScene;
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