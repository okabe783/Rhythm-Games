using UnityEngine;

/// <summary>
/// 入力判定が少し甘くなる。
/// </summary>
[CreateAssetMenu(fileName = "LenientJudgment", menuName = "Ability/LenientJudgment")]
public class LenientJudgment : ScriptableObject, IAbility
{
    [Header("入力判定が少し甘くなるアビリティ")]
    [SerializeField, Header("Perfectの範囲")] private float _perfectTime;
    [SerializeField, Header("Greatの範囲")] private float _greatTime;

    public void Ability()
    {
        var notesJudge = FindObjectOfType<NotesJudge>();
        notesJudge.ChangeTime(_perfectTime, _greatTime);
    }
}