using System.Linq;
using FancyScrollView;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField] private ScrollView _scrollView;
    [SerializeField] private Text _selectedItemInfo;
    [SerializeField] private Window[] _windows;
    private Window _currentWindow;

    private void Start()
    {
        _scrollView.OnSelectionChanged(OnSelectionChanged);

        var items = Enumerable.Range(0, _windows.Length)
            .Select(i => new SellItemData($"Tab {i}"))
            .ToList();

        _scrollView.UpdateData(items);
        _scrollView.SelectCell(0);
    }

    private void OnSelectionChanged(int index, MovementDirection direction)
    {
        _selectedItemInfo.text = $"Selected tab info: index {index}";
        
        if (_currentWindow != null)
        {
            _currentWindow.Out(direction);
            _currentWindow = null;
        }

        if (index >= 0 && index < _windows.Length)
        {
            _currentWindow = _windows[index];
            _currentWindow.In(direction);
        }
    }
}