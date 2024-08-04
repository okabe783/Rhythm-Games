using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 数値を数秒かけて表示する
/// </summary>
public enum TextType
{
    None,
    Score,
    Combo,
    Perfect,
    Great,
    Miss
}

public class NumberTween : MonoBehaviour
{
    #region 宣言部

    [SerializeField] private Text _text = default;
    [SerializeField, Header("表示に何秒かけるか")] private float _duration = 3f;
    private int _endNum = default; // 最終的に表示する数字
    [SerializeField] private TextType _textType = TextType.None;
    private float num = 0;

    /// <summary> 表示するテキストの種類 </summary>
    public TextType TextType => _textType;

    #endregion

    public void NumTween(int endNum)
    {
        _endNum = endNum;
        DOTween.To(() => num, x =>
        {
            num = x;
            UpdateNumberText(); // テキストを更新
        }, _endNum, _duration);
    }

    private void UpdateNumberText()
    {
        _text.text = num.ToString("0");
    }
}