using System;
using FancyScrollView;
using UnityEngine;

/// <summary>曲データ</summary>
internal class Cell : FancyCell<SellItemData, Context>
{
    [SerializeField] private Animator _animator;

    private SoundTable _musicID;
    private float _currentPosition;

    private static readonly int Scroll = Animator.StringToHash("ScrollButton");

    public SoundTable MusicID => _musicID;

    public int GetContextIndex()
    {
        return Context.SelectedIndex;
    }

    // イベントにコールバックを追加する関数
    public void UpdateContext(Action<int> callback)
    {
        Context.OnSelectionChanged += callback;
    }

    private void OnEnable() => UpdatePosition(_currentPosition);

    /// <summary>サウンドIDを更新する</summary>
    /// <param name="itemData"></param>
    public override void UpdateContent(SellItemData itemData)
    {
        _musicID = itemData.SoundTable;
    }

    /// <summary>Positionを更新する</summary>
    public override void UpdatePosition(float position)
    {
        // 現在位置を更新する
        _currentPosition = position;

        if (_animator.isActiveAndEnabled)
        {
            _animator.Play(Scroll, -1, position);
        }

        _animator.speed = 0;
    }
}