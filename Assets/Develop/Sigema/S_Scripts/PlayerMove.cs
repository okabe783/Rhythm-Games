using UnityEngine;

/// <summary>
/// 上中下への移動
/// </summary>
public class PlayerMove : MonoBehaviour, IPlayerMove, IPlayerInput
{
    #region 変数

    [Header("上レーン"), SerializeField] private GameObject _lane = default;
    [Header("上下移動の速度"), SerializeField] private float _speed = 1;
    [Header("到達したと見做す距離"), SerializeField] private float _distance = 0.1f;

    [Header("滞空時間 0:ワンクリック、1:ロングノーツ"), SerializeField]
    private float[] _durationFlights = default;

    private Vector2 _upperLanePos = default; // 上レーンの場所
    private Vector2 _lowerLanePos = default; // 下レーンの場所
    private Vector2 _middleLanePos = default; // 中間の場所
    private Vector2 _targetPos = default; // 上下どちらかの向かう場所
    private bool _toUpper = default; // 上レーンへ向かうか
    private bool _toMiddle = default; // 中間へ向かうか
    private bool _isReach = default; // 到達したか
    private bool _isFixation = false; // 長押し地点で固定するか
    private float[] _timers = default;

    #endregion

    public float Speed => _speed;

    private void Start()
    {
        _toUpper = false;
        _isReach = false;
        var pos = transform.position;
        _lowerLanePos = pos;
        _upperLanePos = new Vector2(pos.x, _lane.transform.position.y);
        _middleLanePos = new Vector2(pos.x, (_upperLanePos.y + _lowerLanePos.y) / 2);
        _timers = new float[2];
    }

    private void Update()
    {
        VerticalMovement();
        var offset = _targetPos - (Vector2)transform.position;
        var sqrLen = offset.sqrMagnitude;
        _isReach = sqrLen < _distance * _distance;
    }

    /// <summary>
    /// 上下移動
    /// </summary>
    public void VerticalMovement()
    {
        if (_toMiddle)
        {
            _targetPos = _middleLanePos;
            _timers[0] += Time.deltaTime;
        }
        else if (_toUpper)
        {
            _targetPos = _upperLanePos;
            _timers[0] += Time.deltaTime;
        }
        else _targetPos = _lowerLanePos;

        if (_timers[0] >= _durationFlights[0])
        {
            DownwardMovement();
            _timers[0] = 0;
        }

        if (!_isReach && !_isFixation)
        {
            var pos = transform.position;
            transform.position = new Vector3(pos.x, _targetPos.y, pos.z);
        }
    }

    /// <summary>
    /// 下降
    /// </summary>
    public void DownwardMovement()
    {
        _toUpper = false;
        _toMiddle = false;
        _isReach = false;
        _timers[0] = 0;
    }

    public void InputUpper()
    {
        _toUpper = true;
        // Debug.Log("upper true");
    }

    public void InputUpperAndLower()
    {
        _toMiddle = true;
    }

    public void InputLower()
    {
        DownwardMovement();
        // Debug.Log("lower true");
    }

    // ロングノーツ中はレーン移動ができないように制限。
    public void InputLongPressStart()
    {
        _isFixation = true;
    }

    public void InputLongPressEnd()
    {
        _isFixation = false;
    }
}