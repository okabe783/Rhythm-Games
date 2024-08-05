using UnityEngine;
using UnityEngine.UI;


/// <summary> スコアをテキストに反映 </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField, Header("スコアのキャンバス")] private Text _scoreText;

    public void SetScore(string text)
    {
        _scoreText.text = text;
    }
}
