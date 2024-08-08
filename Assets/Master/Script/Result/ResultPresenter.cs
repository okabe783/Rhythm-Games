using UnityEngine;
using UniRx;

public class ResultPresenter : MonoBehaviour
{
    [SerializeField] private SetResultValue _setResultValue = default;
    [SerializeField] private NumberTween[] _numberTweens = default;

    private void Start()
    {
        if (_numberTweens.Length == 0) Debug.LogWarning($"{_numberTweens}が空です。");
        foreach (var item in _numberTweens)
        {
            var iReadOnlyReactiveProperty = SetNum(item.TextType);
            iReadOnlyReactiveProperty?.Subscribe(n => item.NumTween(n)).AddTo(this);
        }
    }

    /// <summary>
    /// タイプに応じてViewに入れる数値を変える
    /// </summary>
    private IReadOnlyReactiveProperty<int> SetNum(TextType textType)
    {
        return textType switch
        {
            TextType.Score => _setResultValue.Score,
            TextType.Combo => _setResultValue.Combo,
            TextType.Perfect => _setResultValue.Perfect,
            TextType.Great => _setResultValue.Great,
            TextType.Miss => _setResultValue.Miss,
            TextType.None => null,
            _ => null
        };
    }
}