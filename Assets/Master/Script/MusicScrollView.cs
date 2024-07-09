using System.Collections.Generic;
using UnityEngine;
using FancyScrollView;

public class MusicItemData
{
    public int _musicId;
    public readonly string _musicName;

    public MusicItemData(int id, string musicName)
    {
        _musicId = id;
        this._musicName = musicName;
    }
}

internal class MusicScrollView : FancyScrollView<MusicItemData>
{
    [SerializeField] private Scroller _scroller;
    [SerializeField] private GameObject _cellPrefab;
    
    protected override GameObject CellPrefab => _cellPrefab;

    protected override void Initialize()
    {
        _scroller.OnValueChanged(UpdatePosition);
    }

    public void UpdateData(IList<MusicItemData> items)
    {
        UpdateContents(items);
        _scroller.SetTotalCount(items.Count);
    }
}
