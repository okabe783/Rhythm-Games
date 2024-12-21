using System.Collections.Generic;
using UnityEngine;
using FancyScrollView;
using EasingCore;

/// <summary>セルデータの更新と移動を管理するクラス</summary>
internal class ScrollView : FancyScrollView<SellItemData, Context>
{
    // スクロール位置の制御を行うコンポーネント
    [SerializeField] private Scroller _scroller;
    // ここでセルを格納して表示している
    [SerializeField, Header("曲のPrefabを設定する")] private GameObject _cellPrefab;

    protected override GameObject CellPrefab => _cellPrefab;

    // 初期化
    protected override void Initialize()
    {
        // コールバックの設定
        _scroller.OnValueChanged(UpdatePosition);
        _scroller.OnSelectionChanged(UpdateSelection);
    }
    
    /// <summary>次のインデックスにスクロールする</summary>
    public void SelectNextCell()
    {
        SelectCell(Context.SelectedIndex + 1);
    }

    /// <summary>前のインデックスにスクロールする</summary>
    public void SelectPrevCell()
    {
        SelectCell(Context.SelectedIndex - 1);
    }
    
    /// <summary>セルの更新と移動を管理</summary>
    public void SelectCell(int index)
    {
        UpdateSelection(index);
        _scroller.ScrollTo(index, 0.35f, Ease.OutCubic);
    }
    
    /// <summary>Dataの更新を行う</summary>
    public void UpdateData(IList<SellItemData> items)
    {
        // 表示の更新を行う
        UpdateContents(items);
        // アイテムの総数を設定
        _scroller.SetTotalCount(items.Count);
    }

    // セルのインデックスの更新を行う
    private void UpdateSelection(int index)
    {
        if (Context.SelectedIndex == index) return;
        
        Context.SelectedIndex = index;
        //　Positionを更新
        Refresh();
    }
}