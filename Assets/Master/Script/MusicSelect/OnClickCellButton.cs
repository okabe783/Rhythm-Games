using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class OnClickCellButton : MonoBehaviour
{
    [SerializeField] private Cell _cell;
    [SerializeField] private MoveCellToPoints _moveCellToPoints;
    [SerializeField, Header("曲を拡大したときに表示する戻るボタン")] private Button _backButtonPrefab;
    [SerializeField, Header("曲選択時に出てくる決定ボタン")] private Button _confirmButtonPrefab;
    [SerializeField, Header("セルのボタン")] private Button _musicCellButtonPrefab;
    [SerializeField, Header("保存場所")] private SelectData _selectData;

    private Vector3 _savedPosition;
    private Button _backButtonInstance;
    private Button _confirmButtonInstance;
    private SoundTable _selectedMusicID;

    private void Start()
    {
        _musicCellButtonPrefab.onClick.AddListener(PlayCellSelectionAnimation);
    }

    private void PlayCellSelectionAnimation()
    {
        OnClickCellButton[] cellButtons = FindObjectsOfType<OnClickCellButton>();
        SelectMusicView.I.DeactivateUI();

        foreach (var cellButton in cellButtons)
        {
            if (cellButton == this)
            {
                _savedPosition = _musicCellButtonPrefab.transform.position;
                _musicCellButtonPrefab.transform.DOScale(new Vector2(2, 2), 0.5f);
                _musicCellButtonPrefab.transform.DOMove(new Vector2(-5, 0), 0.5f);
                //UIを表示する
                _backButtonInstance = Instantiate(_backButtonPrefab, transform);
                _confirmButtonInstance = Instantiate(_confirmButtonPrefab, transform);
                _backButtonInstance.onClick.AddListener(ReturnToSavePosition);
                _confirmButtonInstance.onClick.AddListener(ConfirmSelection);
                _selectedMusicID = _cell.MusicID;
            }
            else
            {
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
        //IDを保存して新しいシーンに移動する
        _selectData.SetItemData(_selectedMusicID);
        SceneLoad.I.StartShortLoad("SelectCharacter", "SelectMusicScene");
    }

    private void ReturnToSavePosition()
    {
        OnClickCellButton[] cellButtons = FindObjectsOfType<OnClickCellButton>();
        SelectMusicView.I.ActiveUI();

        foreach (var cellButton in cellButtons)
        {
            cellButton._musicCellButtonPrefab.transform.DOScale(new Vector2(1f, 1f), 0.5f);
            cellButton._musicCellButtonPrefab.transform.DOMove(cellButton._savedPosition, 0.5f);
        }

        Destroy(_backButtonInstance.gameObject);
        Destroy(_confirmButtonInstance.gameObject);
    }

    private Vector2 FindClosestPoint(Vector2 currentPosition, Vector2[] points)
    {
        Vector2 closestPoint = points[0];
        float minDistance = Vector2.Distance(currentPosition, points[0]);

        foreach (var point in points)
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