using UnityEngine;
using UnityEngine.Serialization;

/// <summary> ノーツの判定を行うクラス </summary>
public class NotesJudge : MonoBehaviour
{
    [SerializeField] private GameObject _player = default;
    [SerializeField, Header("ダメージ量")] private int _damageValue = 1;
    private IDamage _damage = default;

    [SerializeField, Header("NotesManagerを入れる")]
    private NotesManager _notesManager;

    [SerializeField, Header("Score")] private ScoreModel _scoreModel;

    [SerializeField, Header("Perfectの範囲")] private float _perfectTime = 0.03f;

    /// <summary> これ以上だとMiss </summary>
    [SerializeField, Header("Greatの範囲")] private float _greatTime = 0.05f;

    /// <summary> 曲が始まった時間 </summary>
    private float _startTime;

    /// <summary> ロングノーツの終わる時間 </summary>
    private float _longNoteFinishTime;

    /// <summary> ロングノーツの流れてくるレーン </summary>
    private int _lane;

    private int _index = 0;

    private void Start()
    {
        _damage = _player.GetComponent<IDamage>();
        _startTime = Time.time + _notesManager.Delay;
        _longNoteFinishTime = -2;
    }

    private void Update()
    {
        CheckPassNote(0, _notesManager.GetTapNotesData(0));
        CheckPassNote(1, _notesManager.GetTapNotesData(1));
        CheckPassNote(0, _notesManager.GetLongNotesData(0).Item1, true);
        CheckPassNote(1, _notesManager.GetLongNotesData(1).Item1, true);
    }

    /// <summary> タップノーツの判定 </summary>
    /// <param name="lane"> (0 = 上, 1 = 下) </param>
    public void TapNoteJudge(int lane)
    {
        float time = _notesManager.GetTapNotesData(lane);
        if (time == -1) return;
        if (Time.time < time + _startTime - _greatTime) return; // Greatの判定よりも早かったら判定しない
        Judgement(Mathf.Abs(Time.time - (time + _startTime)), lane);
    }

    /// <summary> ロングノーツの始まりの判定 </summary>
    /// <param name="lane"> (0 = 上, 1 = 下) </param>
    public void LongNoteStartJudge(int lane)
    {
        float time = _notesManager.GetLongNotesData(lane).Item1;
        if (Time.time < time + _startTime - _greatTime) return; // Greatの判定よりも早かったら判定しない

        float duration = _notesManager.GetLongNotesData(lane).Item2;
        if (duration == -1) return;

        _longNoteFinishTime = time + duration;
        _lane = lane;
        Judgement(Mathf.Abs(Time.time - (time + _startTime)), lane);
        _index = CriSoundManager.Instance.PlaySE("SE_Long_Press");
    }

    /// <summary> ロングノーツの終わりの判定 </summary>
    public void LongNoteFinishJudge()
    {
        float time = _notesManager.GetLongNotesData(_lane).Item1;
        if (_longNoteFinishTime == -2) return;

        CriSoundManager.Instance.StopSE(_index);
        if (Time.time < time + _startTime - _greatTime)
        {
            _notesManager.DeleteNoteData(_lane, true);
            _damage.Damage(_damageValue);
            _scoreModel.AddScore(Rating.Miss);
        }
        else
        {
            Judgement(Mathf.Abs(Time.time - (_longNoteFinishTime + _startTime)), _lane);
        }

        _longNoteFinishTime = -2;
    }

    /// <summary> 評価 </summary>
    /// <param name="timeLag"> どのくらいずれていたか </param>
    /// <param name="lane"> どのレーンか </param>
    private void Judgement(float timeLag, int lane)
    {
        if (timeLag <= _perfectTime)
        {
            // Perfectの処理
            _notesManager.DeleteNoteData(lane, false);
            Debug.Log("perfect");
            _scoreModel.AddScore(Rating.Perfect);
            //Soundを再生
            CriSoundManager.Instance.PlaySE("SE_Perfect", 5f);
        }
        else if (timeLag <= _greatTime)
        {
            // Greatの処理
            _notesManager.DeleteNoteData(lane, false);
            Debug.Log("Great");
            _scoreModel.AddScore(Rating.Great);
            //Soundを再生
            CriSoundManager.Instance.PlaySE("SE_Great", 5f);
        }
    }

    /// <summary> 判定位置を通り過ぎたノーツがあるか </summary>
    /// <param name="lane"> ノーツのレーン </param>
    /// <param name="noteTime"> ノーツが流れてくる時間 </param>
    /// <param name="isLongNote"> ノーツがロングノーツか </param>
    private void CheckPassNote(int lane, float noteTime, bool isLongNote = false)
    {
        if (noteTime != -1)
        {
            if (Time.time - (noteTime + _startTime) > _greatTime)
            {
                _notesManager.DeleteNoteData(lane, true);
                if(isLongNote) _longNoteFinishTime = -2;
                _damage.Damage(_damageValue);
                _scoreModel.AddScore(Rating.Miss);
            }
        }
    }
}