using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _longPressStartTime = 0.4f; // 長押しとみなす入力継続時間
    [SerializeField] private float _longPressEndTime = 4f; // todo:ノーツの終わり時間に該当する
    [SerializeField] private float _interval = default;
    private float _startTimeLongNote = default; // 受け取ったロングノーツの開始時間
    private float _lengthLingNote = default; // 受け取ったロングノーツの長さ
    private float _timer = default;
    private WaitForSeconds _wfs = default;
    private IPlayerInput _iPlayerInput = default;
    private bool _canLongPressFJ = default; // 同時長押しができるか

    private void Start()
    {
        _canLongPressFJ = true;
        _wfs = new WaitForSeconds(_interval);
        _iPlayerInput = transform.GetComponent<IPlayerInput>();
    }

    private void Update()
    {
        // 長押し開始と終了時の処理呼び出し
        if (_timer >= _longPressEndTime)
        {
            _iPlayerInput.InputLongPressEnd();
            _timer = 0;
            _canLongPressFJ = false;
            StartCoroutine(ControlLongPressFJ());
        }
        else if (_timer >= _longPressStartTime)
        {
            // todo : FかJどちらかを長押し中にもう片方の入力を無視。 
            _iPlayerInput.InputLongPressStart();
        }
        // 長押しじゃない入力
        else if (_timer is < 0.2f and > 0)
        {
            if (Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.J))
            {
                _iPlayerInput.InputUpperAndLower();
            }
        }

        // 長押し入力
        if (Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.J))
        {
            if (_canLongPressFJ) _timer += Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.J))
        {
            _timer += Time.deltaTime;
        }

        // キャンセル
        if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.J))
        {
            _iPlayerInput.InputLongPressEnd();
            _timer = 0;
        }

        // 長押しじゃない入力
        if (Input.GetKeyDown(KeyCode.F))
        {
            _iPlayerInput.InputUpper();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            _iPlayerInput.InputLower();
        }
    }

    /// <summary>
    /// 同時長押しの連続使用を制限する。
    /// </summary>
    /// <returns></returns>
    private IEnumerator ControlLongPressFJ()
    {
        yield return _wfs;
        _canLongPressFJ = true;
    }
}