using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region 変数

    [SerializeField] private float _longPressStartTime = 0.4f; // 長押し開始とみなす入力継続時間
    private float _longPressEndTime = 4f; // ノーツの終わり時間
    private float _startTimeLongNote = default; // 受け取ったロングノーツの開始時間
    private float _lengthLongNote = default; // 受け取ったロングノーツの長さ
    private float _timer = default;
    private bool _canLongPress = default; // 長押しができるか
    private bool _canLongPressFJ = default; // 同時長押しができるか
    private IPlayerInput[] _iPlayerInput = default;
    private NotesManager _notesManager = default;

    #endregion

    private void Start()
    {
        _iPlayerInput = transform.GetComponents<IPlayerInput>();
        _notesManager = FindObjectOfType<NotesManager>();
    }

    private void Update()
    {
        if (!_canLongPress) GetLongNoteInfo(); // ロングノーツ押下中は再検索不要

        // ±0.01fの範囲で長押しが開始できるようになる
        if (Time.time <= _startTimeLongNote + 0.01f
            && Time.time >= _startTimeLongNote - 0.01f)
        {
            _canLongPress = true;
        }

        // 長押し終了時の処理呼び出し
        if (_timer >= _longPressEndTime)
        {
            foreach (var item in _iPlayerInput) item.InputLongPressEnd();
            _timer = 0;
            _canLongPressFJ = false;
            _canLongPress = false;
        }
        // 長押し開始時の処理呼び出し
        else if (_timer >= _longPressStartTime)
        {
            // todo : FかJどちらかを長押し中にもう片方の入力を無視したい 
            foreach (var item in _iPlayerInput) item.InputLongPressStart();
        }
        // 長押しじゃない入力 （0超過0.2f未満の範囲）
        else if (_timer is < 0.2f and > 0)
        {
            if (Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.J))
            {
                foreach (var item in _iPlayerInput) item.InputUpperAndLower();
            }
        }

        if (_canLongPress) LongPress();
        Cansel();
        TapPress();
    }

    /// <summary>
    /// 長押しじゃない入力
    /// </summary>
    private void TapPress()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (var item in _iPlayerInput) item.InputUpper();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            foreach (var item in _iPlayerInput) item.InputLower();
        }
    }

    /// <summary>
    /// 長押し入力
    /// </summary>
    private void LongPress()
    {
        if (Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.J))
        {
            if (_canLongPressFJ) _timer += Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.F))
        {
            _timer += Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.J))
        {
            _timer += Time.deltaTime;
        }
    }

    /// <summary>
    /// キャンセル
    /// </summary>
    private void Cansel()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            foreach (var item in _iPlayerInput) item.InputLongPressEnd();
            _timer = 0;
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            foreach (var item in _iPlayerInput) item.InputLongPressEnd();
            _timer = 0;
        }
    }

    /// <summary>
    /// ロングノーツの開始時間・長さを取得
    /// </summary>
    private void GetLongNoteInfo()
    {
        if (_notesManager == null) return;

        var upper = _notesManager.GetLongNotesData(0);
        var lower = _notesManager.GetLongNotesData(1);
        // 先に流れて来る方を取得
        if (upper.Item1 <= lower.Item1)
        {
            _startTimeLongNote = upper.Item1;
            _lengthLongNote = upper.Item2;
        }

        // 同時に流れてきたとき
        // todo:上と下で長さが異なるケースを想定していない
        if (upper.Item1 == lower.Item1)
        {
            _canLongPressFJ = true;
        }

        _longPressEndTime = _lengthLongNote - _longPressStartTime;
    }
}