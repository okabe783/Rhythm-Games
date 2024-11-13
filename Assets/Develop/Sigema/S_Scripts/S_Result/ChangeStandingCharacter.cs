using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スコアが一定のラインに満たなかったときに、立ち絵を変更する
/// </summary>
public class ChangeStandingCharacter : MonoBehaviour
{
    [SerializeField, Header("変更する画像")] private Sprite _sprite = default;

    [SerializeField, Header("対象")] private GameObject _target = default;

    // 仮
    [SerializeField, Header("スコアの保存場所")] private TestSaveData _testSaveData = default;

    private void Start()
    {
        if (_testSaveData.Score < 200)
        {
            _target.GetComponent<Image>().sprite = _sprite;
        }
    }
}