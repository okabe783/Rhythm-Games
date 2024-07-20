using UnityEngine;
using UnityEngine.Serialization;

/// <summary> ノーツの判定を行うクラス </summary>
public class NotesJudge : MonoBehaviour
{
    [SerializeField, Header("NotesManagerを入れる")]
    private NotesManager _notesManager;
    
    [SerializeField, Header("Perfectの範囲")] 
    private float _perfectTime = 0.03f;
 
    /// <summary> これ以上だとMiss </summary>
    [SerializeField, Header("Greatの範囲")] 
    private float _greatTime = 0.05f;
    
    /// <summary> 曲が始まった時間 </summary>
    private float _statTime;
    
    /// <summary> ロングノーツの終わる時間 </summary>
    private float _longNoteFinishTime;

    /// <summary> ロングノーツの流れてくるレーン </summary>
    private int _lane;
    
    void Start()
    {
        _statTime = Time.time;
        _longNoteFinishTime = -2;
    }
    
    void Update()
    {
        float upTapNoteTime = _notesManager.GetTapNotesData(0);
        float downTapNoteTime = _notesManager.GetTapNotesData(1);
        float upLongNoteTime = _notesManager.GetLongNotesData(0).Item1;
        float downLongNoteTime = _notesManager.GetLongNotesData(1).Item1;
        if (upTapNoteTime != -1)
        {
            if (Time.time - (upTapNoteTime + _statTime) > _greatTime)
            {
                //ToDo:Missの処理
                _notesManager.DeleteNoteData(0);
                Debug.Log("miss");
            }
        }

        if (downTapNoteTime != -1)
        {
            if (Time.time - (downTapNoteTime + _statTime) > _greatTime)
            {
                //ToDo:Missの処理
                _notesManager.DeleteNoteData(1);
                Debug.Log("miss");
            }
        }
        
        if (upLongNoteTime != -1)
        {
            if (Time.time - (upLongNoteTime + _statTime) > _greatTime)
            {
                //ToDo:Missの処理
                _notesManager.DeleteNoteData(0);
                _longNoteFinishTime = -2;
                Debug.Log("miss");
            }
        }
        
        if (downLongNoteTime != -1)
        {
            if (Time.time - (downLongNoteTime + _statTime) > _greatTime)
            {
                //ToDo:Missの処理
                _notesManager.DeleteNoteData(1);
                _longNoteFinishTime = -2;
                Debug.Log("miss");
            }
        }
    }

    /// <summary> タップノーツの判定 </summary>
    /// <param name="getLane"> (0 = 上, 1 = 下) </param>
    public void TapNoteJudge(int getLane)
    {
        float time = _notesManager.GetTapNotesData(getLane);
        if (time == -1) { return; }
        if (Time.time <  time + _statTime - _greatTime) { return; } // Greatの判定よりも早かったら判定しない
        Judgement(Mathf.Abs(Time.time - (time + _statTime)), getLane);
    }
    
    /// <summary> ロングノーツの始まりの判定 </summary>
    /// <param name="getLane"> (0 = 上, 1 = 下) </param>
    public void LongNoteStartJudge(int getLane)
    {
        float time = _notesManager.GetLongNotesData(getLane).Item1;
        if (Time.time < time + _statTime - _greatTime) { return; } // Greatの判定よりも早かったら判定しない
        float duration = _notesManager.GetLongNotesData(getLane).Item2;
        if (duration == -1) { return; }
        _longNoteFinishTime = time + duration;
        _lane = getLane;
        Judgement(Mathf.Abs(Time.time - (time + _statTime)), getLane);
    }
    
    /// <summary> ロングノーツの終わりの判定 </summary>
    public void LongNoteFinishJudge()
    {
        float time = _notesManager.GetLongNotesData(_lane).Item1;
        if (_longNoteFinishTime == -2) { return; }
        if (Time.time < time + _statTime - _greatTime)
        {
            //ToDo:Missの処理
            _longNoteFinishTime = -2;
            _notesManager.DeleteNoteData(_lane);
            Debug.Log("miss");
        }
        else
        {
            Judgement(Mathf.Abs(Time.time - (_longNoteFinishTime + _statTime)), _lane);
            _longNoteFinishTime = -2;
        }
    }

    /// <summary> 評価 </summary>
    /// <param name="timeLag"> どのくらいずれていたか </param>
    /// <param name="lane"> どのレーンか </param>
    private void Judgement(float timeLag, int lane)
    {
        if (timeLag <= _perfectTime)
        {
            // Perfectの処理
            _notesManager.DeleteNoteData(lane);
            Debug.Log("perfect");
        }
        else if (timeLag <= _greatTime)
        {
            // Greatの処理
            _notesManager.DeleteNoteData(lane);
            Debug.Log("Great");
        }
    }
}
