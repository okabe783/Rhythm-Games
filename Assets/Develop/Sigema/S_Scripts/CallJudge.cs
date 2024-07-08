using UnityEngine;

/// <summary>
/// 入力に応じて判定を呼び出す
/// </summary>
public class CallJudge : MonoBehaviour, IPlayerInput
{
    private NotesJudge _notesJudge = default;

    private void Start()
    {
        _notesJudge = FindObjectOfType<NotesJudge>();
    }

    public void InputUpper()
    {
        _notesJudge.TapNoteJudge(0);
    }

    public void InputUpperAndLower()
    {
        _notesJudge.TapNoteJudge(0);
        _notesJudge.TapNoteJudge(1);
    }

    public void InputLower()
    {
        _notesJudge.TapNoteJudge(1);
    }

    public void InputLongPressStart()
    {
        if (Input.GetKey(KeyCode.K)) _notesJudge.LongNoteStartJudge(0);
        if (Input.GetKey(KeyCode.J)) _notesJudge.LongNoteStartJudge(1);
    }

    public void InputLongPressEnd()
    {
        _notesJudge.LongNoteFinishJudge();
    }
}