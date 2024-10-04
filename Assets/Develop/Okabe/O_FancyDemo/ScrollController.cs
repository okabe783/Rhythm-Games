using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    [SerializeField] private ScrollView _scrollView;
    [SerializeField] private Button _prevCellButton;
    [SerializeField] private Button _nextCellButton;
    [SerializeField, Header("曲の数")] private int _musicList;

    private void Start()
    {
        _prevCellButton.onClick.AddListener(_scrollView.SelectPrevCell);
        _nextCellButton.onClick.AddListener(_scrollView.SelectNextCell);

        var items = Enumerable.Range(0, _musicList)
            .Select(i => new SellItemData(i))
            .ToArray();
        
        _scrollView.UpdateData(items);
        _scrollView.SelectCell(0);
    }
}