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
    private IPlayerInput[] _iPlayerInput = default;
    private NotesManager _notesManager = default;
    private bool _isLongPressF = default;
    private bool _isLongPressJ = default;
    private float _timeFKeyPressed = -1f;
    private float _timeJKeyPressed = -1f;
    private float _startTime = default; // インゲームが開始された時間
    private bool _canLongPressStart = default; // 長押しを開始できる範囲か

    #endregion

    /// <summary> 長押し中か </summary>
    public bool IsLongPressF => _isLongPressF;

    /// <summary> 長押し中か </summary>
    public bool IsLongPressJ => _isLongPressJ;

    private void Start()
    {
        _longPressStartTime = 0.1f;
        _iPlayerInput = transform.GetComponents<IPlayerInput>();
        _notesManager = FindObjectOfType<NotesManager>();
        _startTime = Time.time;
    }

    private void Update()
    {
        if (_timer > _longPressStartTime)
        {
            if (Input.GetKey(KeyCode.F)) _isLongPressF = true;
            if (Input.GetKey(KeyCode.J)) _isLongPressJ = true;
        }
        else
        {
            _isLongPressF = false;
            _isLongPressJ = false;
        }

        // ロングノーツの終端を過ぎたとき
        var num = _startTimeLongNote + _lengthLongNote;
        if (Time.time > num + _startTime + 0.1f)
        {
            // 次のロングノーツの始端が来るまで偽
            _canLongPress = false;
            _timer = 0;
        }

        if (!_canLongPress) GetLongNoteInfo(); // ロングノーツ押下中は再検索不要

        // ±0.1fの範囲でのみ長押しが「開始」できるようになる
        if (Time.time <= _startTimeLongNote + _startTime + 0.1f
            && Time.time >= _startTimeLongNote + _startTime - 0.1f)
        {
            _canLongPressStart = true;
        }
        else _canLongPressStart = false;

        // 長押し終了時の処理呼び出し
        if (_timer >= _longPressEndTime)
        {
            foreach (var item in _iPlayerInput) item.InputLongPressEnd();
            _timer = 0;
            _canLongPress = false;
        }

        // 長押し開始時の処理呼び出し
        if (_canLongPressStart)
        {
            LongPress(); // 開始判定の数秒は長押しができる
            if (_timer >= _longPressStartTime)
            {
                // todo : FかJどちらかを長押し中にもう片方の入力を無視したい 
                foreach (var item in _iPlayerInput) item.InputLongPressStart();
                _canLongPress = true;
            }
        }

        if (_canLongPress) LongPress();

        CanselF();
        CanselJ();
        TapPress();
    }

    /// <summary>
    /// タップ用
    /// </summary>
    private void TapPress()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            _timeJKeyPressed = Time.time;
            foreach (var item in _iPlayerInput) item.InputLower();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _timeFKeyPressed = Time.time;
            foreach (var item in _iPlayerInput) item.InputUpper();
        }

        // 両方のキーが押された場合の時間差を計算
        if (_timeFKeyPressed >= 0 && _timeJKeyPressed >= 0)
        {
            // 入力時間差
            var timeDifference = Mathf.Abs(_timeFKeyPressed - _timeJKeyPressed);
            if (timeDifference <= 0.05f)
            {
                foreach (var item in _iPlayerInput) item.InputUpperAndLower();
            }

            // 計算後にタイムスタンプをリセットする
            _timeFKeyPressed = -1f;
            _timeJKeyPressed = -1f;
        }
    }


    /// <summary>
    /// 長押し入力
    /// </summary>
    private void LongPress()
    {
        if (Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.J))
        {
            _timer += Time.deltaTime;
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

    private void CanselF()
    {
        if (!_isLongPressF || _isLongPressJ) return;
        if (Input.GetKeyUp(KeyCode.F))
        {
            foreach (var item in _iPlayerInput) item.InputLongPressEnd();
            _timer = 0;
            _canLongPress = false;
        }
    }

    private void CanselJ()
    {
        if (!_isLongPressJ || _isLongPressF) return;
        if (Input.GetKeyUp(KeyCode.J))
        {
            foreach (var item in _iPlayerInput) item.InputLongPressEnd();
            _timer = 0;
            _canLongPress = false;
        }
    }

    /// <summary>
    /// ロングノーツの開始時間・長さを取得
    /// </summary>
    private void GetLongNoteInfo()
    {
        var upper = _notesManager.GetLongNotesData(0);
        var lower = _notesManager.GetLongNotesData(1);
        var start = -1f;
        var length = -1f;
        // 先に流れて来る方を取得

        if (upper.Item1 == -1 && lower.Item1 == -1)
        {
            return; // ロングノーツなし
        }

        if (upper.Item1 == -1 || lower.Item1 == -1)
        {
            if (upper.Item1 > lower.Item1)
            {
                start = upper.Item1;
                length = upper.Item2;
            }
            else
            {
                start = lower.Item1;
                length = lower.Item2;
            }
        }
        else
        {
            // どちらも有効なら先に来る方を取得
            if (upper.Item1 <= lower.Item1)
            {
                start = upper.Item1;
                length = upper.Item2;
            }
            else
            {
                start = lower.Item1;
                length = lower.Item2;
            }
        }

        _startTimeLongNote = start + _notesManager.Delay;
        _lengthLongNote = length;

        // todo:上と下で長さが異なるケースを想定していない

        _longPressEndTime = _lengthLongNote + _longPressStartTime;
    }
}