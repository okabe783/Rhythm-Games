using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    [SerializeField] private ScrollView _scrollView;
    [SerializeField] private Button _prevCellButton;
    [SerializeField] private Button _nextCellButton;
    [SerializeField] private SoundTableList _soundTableList;
   
    private void Start()
    {
        _prevCellButton.onClick.AddListener(_scrollView.SelectPrevCell);
        _nextCellButton.onClick.AddListener(_scrollView.SelectNextCell);

        var items = Enumerable.Range(0, _soundTableList.SoundTables.Count)
            .Select(i =>new SellItemData(_soundTableList.SoundTables[i])) 
            .ToArray();
        
        _scrollView.UpdateData(items);
        _scrollView.SelectCell(0);
    }
}