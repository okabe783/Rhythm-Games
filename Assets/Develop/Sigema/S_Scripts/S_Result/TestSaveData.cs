using UnityEngine;

/// <summary>
/// テスト用のスコア等を保存しているクラス
/// </summary>
public class TestSaveData : MonoBehaviour
{
    [SerializeField] private int _score = default;
    [SerializeField] private int _combo = default;
    [SerializeField] private int _perfect = default;
    [SerializeField] private int _great = default;
    [SerializeField] private int _miss = default;

    public int Score => _score;
    public int Combo => _combo;
    public int Perfect => _perfect;
    public int Great => _great;
    public int Miss => _miss;
}