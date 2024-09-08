using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 仮；リザルトテキストの管理
/// </summary>
public class ResultViewManager : MonoBehaviour
{
    [SerializeField, Header("保存場所")] private SelectData _selectData;
    [SerializeField, Header("曲名テキスト")] private Text _musicTitleText;
    // [SerializeField, Header("スコアテキスト")]　private Test _scoreText;
    // [SerializeField, Header("ハイスコアテキスト")]　private Test _highScoreText;
    // [SerializeField, Header("コンボ数テキスト")]　private Test _comboCountText;
    // [SerializeField, Header("パーフェクト数テキスト")]　private Test _perfectCountText;
    // [SerializeField, Header("グレート数テキスト")]　private Test _greatCountText;
    // [SerializeField, Header("ミス数テキスト")]　private Test _missCountText;
    // [SerializeField, Header("パス数テキスト")]　private Test _passCountText;
    
    void Start()
    {
        if (_selectData.ItemData == null) return; // Resultシーン単体で実行する時用
        _musicTitleText.text = _selectData.ItemData.SoundName;
    }
}
