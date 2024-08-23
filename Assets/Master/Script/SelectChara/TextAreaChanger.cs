using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 対象：スクリプトをアタッチしたもの
/// カーソルをかざしたときに「スキル名・スキルの説明を表示する」
/// </summary>
public class TextAreaChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Header("キャラID")] private int _charaId = default;
    private SetCharaInfo _setCharaInfo = default;
    private Text _text = default;

    private void Start()
    {
        _setCharaInfo = FindObjectOfType<SetCharaInfo>();
        var child = GameObject.Find("SkillInfoTextArea").transform.GetChild(0);
        _text = child.GetComponent<Text>();

        if (_setCharaInfo == null)
        {
            Debug.LogWarning($"{_setCharaInfo.name}が見つかりませんでした。");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.text = _setCharaInfo.GetSkillName(_charaId);
        _text.text += "\n\n" + _setCharaInfo.GetSkillInfo(_charaId);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.text = "スキルの説明が表示されます。";
    }
}