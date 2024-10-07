using UnityEngine;

/// <summary> InGameの終わりを判定する </summary>
public class GameEndChecker : MonoBehaviour
{
    [SerializeField] private NotesManager _notesManager;

    [SerializeField] private float _untilChange;
    
    private bool _upTapNotefinish; // 上タップノーツはもうないか
    private bool _downTapNotefinish; // 下のタップノーツはもうないか
    private bool _upLongNotefinish; // 上のロングノーツはもうないか
    private bool _downLongNotefinish; // 下のロングノーツはもうないか

    private bool _ingameFinish; // ingameが終わったか（ノーツが一つも残っていない）
    private float _timer = default; // 生成されるまで待つためのタイマー

    private void Start()
    {
        _upTapNotefinish = false;
        _downTapNotefinish = false;
        _upLongNotefinish = false;
        _downLongNotefinish = false;
        _ingameFinish = false;
    }
    
    private void Update()
    {
        if (_timer < _notesManager.Delay + 2) _timer += Time.deltaTime;
        if (_timer > _notesManager.Delay + 1)
        {
            if (!_ingameFinish)
            {
                _upTapNotefinish = FinishCheck(_notesManager.GetTapNotesData(0));
                _downTapNotefinish = FinishCheck(_notesManager.GetTapNotesData(1));
                _upLongNotefinish = FinishCheck(_notesManager.GetLongNotesData(0).Item1);
                _downLongNotefinish = FinishCheck(_notesManager.GetLongNotesData(1).Item1);
                
                if (_upTapNotefinish && _downTapNotefinish && _upLongNotefinish && _downLongNotefinish)
                {
                    _ingameFinish = true;
                    Invoke("SceneChange", _untilChange);
                }
            }
        }
    }

    private bool FinishCheck(float noteTime)
    {
        if (noteTime == -1) return true;
        return false;
    }
    
    private void SceneChange()
    {
        SceneLoad.I.OnChangeScene("Result","MainStage");
    }
}

