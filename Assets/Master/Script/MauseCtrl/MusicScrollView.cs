using System;
using UnityEngine;
using FancyScrollView;
using System.Collections.Generic;
using EasingCore;

/// <summary>保存された音楽データ</summary>
public class MusicItemData
{
    public int TableId { get; }
    public int PreviewTime { get; }
    public string SoundName { get; }
    public string SelectScene { get; }

    //読み込まれたデータを取得
    public MusicItemData(SoundTable soundTable)
    {
        TableId = soundTable.TableId;
        PreviewTime = soundTable.PreviewTime;
        SoundName = soundTable.SoundName;
        SelectScene = soundTable.SelectScene;
    }
}

internal class MusicScrollView : FancyScrollView<MusicItemData,Context>
{
    [SerializeField] private Scroller _scroller; //スクロール位置の制御を行うコンポーネント
    
    [SerializeField] private GameObject _cellPrefab; //セルのプレハブ

    private Action<MusicItemData> _onSelectionChanged;

    [SerializeField] private SelectData _selectData;
    protected override GameObject CellPrefab => _cellPrefab;

    //スクロールの位置が変わるたびにUpdatePositionが呼ばれるように設定
    protected override void Initialize()
    {
        base.Initialize(); //基底クラスにアクセスして初期化

        Context.OnClickSellIndex = SelectCell; //SelectSellを登録
        
        _scroller.OnValueChanged(UpdatePosition);　//スクロール位置が変わるたびにUpdatePositionを呼び出す
        _scroller.OnSelectionChanged(UpdateSelection);　//選択が変更されたときにUpdateSelectionを呼び出す
    }

    //選択されたセルのインデックスを更新
    private void UpdateSelection(int index)
    {
        if(Context._selectIndex == index) return;

        Context._selectIndex = index;
        Refresh();　//セルのレイアウトと表示内容を強制的に更新
        
        _onSelectionChanged?.Invoke(ItemsSource[index]);
    }

    //ScrollViewの内容を更新
    public void UpdateData(IList<MusicItemData> items)
    {
        //新しいデータを設定してスクロールの総数を更新
        UpdateContents(items);
        _scroller.SetTotalCount(items.Count);
        CriSoundManager.Instance.PlaySE("SE_Scroll");
    }

    //選択が変更された時に呼び出される
    public void OnSelectionChanged(Action<MusicItemData> callback)
    {
        _onSelectionChanged = callback;
    }
    
    //セルが所持している曲のDataを保存してシーンを遷移
    public void OnSelectSell()
    {
        if (Context._selectIndex >= 0 && Context._selectIndex < ItemsSource.Count)
        {
            _selectData.ItemData = ItemsSource[Context._selectIndex];
            Debug.Log($"Saved selected music data: {_selectData.ItemData.SoundName}");

            //次のシーンに切り換える
            if (SceneLoad.Instance != null)
                SceneLoad.Instance.OnChangeScene("SelectChara","SelectMusicScene");
            else
                Debug.LogError("SceneLoadインスタンスを取得できていません");
            
        }
        else
            Debug.LogError("No music data selected to save.");
    }

    //次のセルを選択
    public void SelectNextSell()
    {
        SelectCell(Context._selectIndex + 1);
    }
    //前のセルを選択
    public void SelectPrevCell()
    {
        SelectCell(Context._selectIndex - 1);
    }
    public void SelectCell(int index)
    {
        //範囲外のインデックス、すでに選択されているインデックスは無視
        if(index < 0 || index >= ItemsSource.Count || index == Context._selectIndex)
            return;
        
        UpdatePosition(index);
        _scroller.ScrollTo(index,0.35f,Ease.OutCubic);
    }
}