using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>Cell(曲リスト)をクリックした時の処理を管理する</summary>
public class OnClickCellButton : MonoBehaviour
{
    [SerializeField] private Cell _cell;
    [SerializeField] private MoveCellToPoints _moveCellToPoints;

    [SerializeField, Header("曲を選択した時に表示する戻るボタン")] private Button _backButtonPrefab;
    [SerializeField, Header("曲選択時に出てくる決定ボタン")] private Button _confirmButtonPrefab;
    [SerializeField, Header("セルのボタン")] private Button _musicCellButtonPrefab;
    [SerializeField, Header("保存場所")] private SelectData _selectData;

    private Vector3 _savedPosition;
    private Button _backButtonInstance;
    private Button _confirmButtonInstance;
    private SoundTable _selectedMusicID;

    private OnClickCellButton[] _cellButtons;

    private Context _context;
    
    private void Start()
    {
        // ボタンにイベントを登録
        _musicCellButtonPrefab.onClick.AddListener(PlayCellSelectionAnimation);
        // インスタンス化される全ての曲リストを配列に格納する
        _cellButtons = FindObjectsOfType<OnClickCellButton>();
        
        _cell.UpdateContext(UpdateButtonInteractivity);
    }
    
    // 真ん中のセルだけクリック可能にする
    private void UpdateButtonInteractivity(int index)
    {
        foreach (OnClickCellButton cellButton in _cellButtons)
        {
            //　セルの
            cellButton._musicCellButtonPrefab.interactable = cellButton._cell.Index == index;
        }
    }

    // ボタンが押された時の処理
    private void PlayCellSelectionAnimation()
    {
        int context = _cell.GetContextIndex();

        // 押されたセルが真ん中ではない場合、何もしない
        if (_cell.Index != context)
        {
            Debug.Log("このセルはクリックできません");
            return;
        }

        SelectMusicView.I.DeactivateUI();

        foreach (OnClickCellButton cellButton in _cellButtons)
        {
            // 真ん中のセルに対する処理
            if (cellButton._cell.Index == context)
            {
                _savedPosition = _musicCellButtonPrefab.transform.position;
                _musicCellButtonPrefab.transform.DOScale(new Vector2(2, 2), 0.5f);
                _musicCellButtonPrefab.transform.DOMove(new Vector2(-5, 0), 0.5f);

                // UIを表示する
                _backButtonInstance = Instantiate(_backButtonPrefab, transform);
                _confirmButtonInstance = Instantiate(_confirmButtonPrefab, transform);
                _backButtonInstance.onClick.AddListener(ReturnToSavePosition);
                _confirmButtonInstance.onClick.AddListener(ConfirmSelection);
                _selectedMusicID = _cell.MusicID;
            }
            else
            {
                // 他のセルのボタンを無効化
                cellButton._musicCellButtonPrefab.interactable = false;

                cellButton._savedPosition = cellButton._musicCellButtonPrefab.transform.position;
                Vector2[] points = _moveCellToPoints.GetCircularPoints();
                Vector2 currentPosition = cellButton.transform.position;
                Vector2 closestPoint = FindClosestPoint(currentPosition, points);
                cellButton._musicCellButtonPrefab.transform.DOMove(closestPoint, 1f).SetEase(Ease.InOutQuad);
            }
        }
    }

    private void ConfirmSelection()
    {
        //　IDを保存して新しいシーンに移動する
        _selectData.SetItemData(_selectedMusicID);
        SceneLoad.I.StartShortLoad("SelectCharacter", "SelectMusicScene");
    }

    private void ReturnToSavePosition()
    {
        SelectMusicView.I.ActiveUI();

        foreach (OnClickCellButton cellButton in _cellButtons)
        {
            cellButton._musicCellButtonPrefab.transform.DOScale(new Vector2(1f, 1f), 0.5f);
            cellButton._musicCellButtonPrefab.transform.DOMove(cellButton._savedPosition, 0.5f);
        }

        // ボタンのクリック可能状態を更新
        UpdateButtonInteractivity(_cell.Index);

        Destroy(_backButtonInstance.gameObject);
        Destroy(_confirmButtonInstance.gameObject);
    }

    // 最も近い点を見つける
    private Vector2 FindClosestPoint(Vector2 currentPosition, Vector2[] points)
    {
        Vector2 closestPoint = points[0];
        float minDistance = Vector2.Distance(currentPosition, points[0]);

        foreach (Vector2 point in points)
        {
            float distance = Vector2.Distance(currentPosition, point);
            if (distance < minDistance)
            {
                closestPoint = point;
                minDistance = distance;
            }
        }

        return closestPoint;
    }
}