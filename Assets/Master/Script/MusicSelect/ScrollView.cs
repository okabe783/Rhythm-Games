using System.Collections.Generic;
using UnityEngine;
using FancyScrollView;
using EasingCore;

internal class ScrollView : FancyScrollView<SellItemData, Context>
{
    [SerializeField] private Scroller _scroller;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Animator _rudderAnimator;
    protected override GameObject CellPrefab => _cellPrefab;

    protected override void Initialize()
    {
        base.Initialize(); 
        _scroller.OnValueChanged(UpdatePosition);
        _scroller.OnSelectionChanged(UpdateSelection);
    }

    private void UpdateSelection(int index)
    {
        if (Context.SelectedIndex == index) return;

        Context.SelectedIndex = index;
        Refresh();
    }

    public void UpdateData(IList<SellItemData> items)
    {
        UpdateContents(items);
        _scroller.SetTotalCount(items.Count);
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
        UpdateSelection(index);
        _scroller.ScrollTo(index,0.35f,Ease.OutCubic);
    }
}