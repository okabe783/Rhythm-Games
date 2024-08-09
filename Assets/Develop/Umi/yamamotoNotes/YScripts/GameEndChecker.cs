using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (!_ingameFinish)
        {
            if (!_upTapNotefinish)
            {
                if (_notesManager.GetTapNotesData(0) == -1)
                {
                    _upTapNotefinish = true;
                }
            }

            if (!_downTapNotefinish)
            {
                if (_notesManager.GetTapNotesData(1) == -1)
                {
                    _downTapNotefinish = true;
                }
            }

            if (!_upLongNotefinish)
            {
                if (_notesManager.GetLongNotesData(0).Item1 == -1)
                {
                    _upLongNotefinish = true;
                }
            }

            if (!_downLongNotefinish)
            {
                if (_notesManager.GetLongNotesData(1).Item1 == -1)
                {
                    _downLongNotefinish = true;
                }
            }

            if (_upTapNotefinish && _downTapNotefinish && _upLongNotefinish && _downLongNotefinish)
            {
                _ingameFinish = true;
                Invoke("SceneChange", _untilChange);
            }
        }
    }

    private void SceneChange()
    {
        SceneLoad.Instance.OnChangeScene("Result","Stage1_Beta");
    }
}

