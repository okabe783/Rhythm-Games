using UnityEngine;

public class NotesJudge : MonoBehaviour
{
    [SerializeField, Header("NotesManagerを入れる")]
    private NotesManager _notesManager;

    /// <summary> 曲が始まった時間 </summary>
    private float _statTime;
    
    void Start()
    {
        _statTime = Time.time;
    }
    
    void Update()
    {
        float tapNoteTime = _notesManager.GetTapNotesData(0);
        if (Time.time > tapNoteTime + 0.15f + _statTime)
        {
            // Missの処理
            
            _notesManager.DeleteNoteData(0);
        }
    }

    public void TapNoteJudge(int getLane)
    {
        float time = _notesManager.GetTapNotesData(getLane);
        Judgement(Mathf.Abs(Time.time - (time + _statTime)), 0);
    }
    
    public void LongNoteJudge(int getLane)
    {
        float time = _notesManager.GetLongNotesData(getLane).Item1;
        float duration = _notesManager.GetLongNotesData(getLane).Item2;
        Judgement(Mathf.Abs(Time.time - (time + _statTime)), 0);
    }

    /// <summary> 評価 </summary>
    /// <param name="timeLag"> どのくらいずれていたか </param>
    /// <param name="lane"> どっちのレーンか（0 = 上, 1 = 下） </param>
    private void Judgement(float timeLag, int lane)
    {
        if (timeLag <= 0.05f)
        {
            // Perfectの処理
        }
        else if (timeLag <= 0.08f)
        {
            // Greatの処理
        }
        else if (timeLag <= 0.15f)
        {
            // Greatの処理
        }
        
        _notesManager.DeleteNoteData(lane);
    }
}
