using UniRx;
using UnityEngine;

/// <summary>
/// 保存しているクラスから値を取得して、監視対象を設定する。
/// </summary>
public class SetResultValue : MonoBehaviour
{
    #region 宣言部

    //todo: スコア等を保存しているクラス
    [SerializeField] private TestSaveData _testSaveData = default; // テスト用
    private IntReactiveProperty _score = new IntReactiveProperty();
    private IntReactiveProperty _combo = new IntReactiveProperty();
    private IntReactiveProperty _perfect = new IntReactiveProperty();
    private IntReactiveProperty _great = new IntReactiveProperty();
    private IntReactiveProperty _miss = new IntReactiveProperty();
    public IReadOnlyReactiveProperty<int> Score => _score;
    public IReadOnlyReactiveProperty<int> Combo => _combo;
    public IReadOnlyReactiveProperty<int> Perfect => _perfect;
    public IReadOnlyReactiveProperty<int> Great => _great;
    public IReadOnlyReactiveProperty<int> Miss => _miss;

    #endregion

    private void Start()
    {
        _score.Value = _testSaveData.Score;
        _combo.Value = _testSaveData.Combo;
        _perfect.Value = _testSaveData.Perfect;
        _great.Value = _testSaveData.Great;
        _miss.Value = _testSaveData.Miss;
    }
}