using UnityEngine;

/// <summary>
/// スコア加算量を変更する
/// </summary>
[CreateAssetMenu(fileName = "ScoreUp", menuName = "Ability/ScoreUp")]
public class ScoreUp : ScriptableObject, IAbility
{
    [Header("スコア加算量を変更するアビリティ　※N倍")] [SerializeField, Header("Perfectのスコア加算量")]
    private float _perfect;

    [SerializeField, Header("Greatのスコア加算量")]
    private float _great;

    public void Ability()
    {
        var scoreModel = FindObjectOfType<ScoreModel>();
        scoreModel.ChangePointValue(_perfect, _great);
    }
}