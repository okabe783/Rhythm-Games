using System;
using System.Collections.Generic;
using UnityEngine;
using EasingCore;
using FancyScrollView;

internal class ScrollView : FancyScrollView<SellItemData, Context>
{
    [SerializeField] private Scroller _scroller;
    [SerializeField] private GameObject _cellPrefab;

    private Action<int, MovementDirection> _onSelectionChanged;

    protected override GameObject CellPrefab => _cellPrefab;

    protected override void Initialize()
    {
        base.Initialize();

        Context.OnCellClicked = SelectCell;

        _scroller.OnValueChanged(UpdatePosition);
        _scroller.OnSelectionChanged(UpdateSelection);
    }

    private void UpdateSelection(int index)
    {
        if (Context.SelectedIndex == index) return;

        var direction = _scroller.GetMovementDirection(Context.SelectedIndex, index);

        Context.SelectedIndex = index;
        Refresh();

        _onSelectionChanged?.Invoke(index, direction);
    }

    public void UpdateData(IList<SellItemData> items)
    {
        UpdateContents(items);
        _scroller.SetTotalCount(items.Count);
    }

    public void OnSelectionChanged(Action<int, MovementDirection> callback)
    {
        _onSelectionChanged = callback;
    }

    public void SelectNextCell()
    {
        SelectCell(Context.SelectedIndex + 1);
    }

    public void SelectPrevCell()
    {
        SelectCell(Context.SelectedIndex - 1);
    }

    public void SelectCell(int index)
    {
        if (index < 0 || index >= ItemsSource.Count || index == Context.SelectedIndex)
        {
            return;
        }

        _scroller.ScrollTo(index, 0.35f, Ease.OutCubic);
    }
}