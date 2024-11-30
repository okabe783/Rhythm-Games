using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// インスタンス開始時にスクロールに必要な情報を渡す
public class ScrollController : MonoBehaviour
{
    [SerializeField] private ScrollView _scrollView;
    [SerializeField] private SoundTableList _soundTableList;
    [SerializeField] private RudderAnimation _rudderAnimation;

    [Header("次にスクロールするためのボタン")] [SerializeField] private Button _prevCellButton;

    [SerializeField] private Button _nextCellButton;


    private void Awake()
    {
        // Buttonにイベントを追加する
        _prevCellButton.onClick.AddListener(() =>
        {
            _scrollView.SelectPrevCell();
            _rudderAnimation.OnRotateLeft();
        });
        _nextCellButton.onClick.AddListener(() =>
        {
            _scrollView.SelectNextCell();
            _rudderAnimation.OnRotateRight();
        });
    }

    private void Start()
    {
        // SelectItem型のインスタンスを生成
        SellItemData[] items = Enumerable.Range(0, _soundTableList.SoundTables.Count)
            .Select(i => new SellItemData(_soundTableList.SoundTables[i]))
            .ToArray();

        _scrollView.UpdateData(items);
        _scrollView.SelectCell(0);
    }
}