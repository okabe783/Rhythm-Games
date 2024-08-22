using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 対象：スクリプトをアタッチしたもの
/// カーソルをかざしたときに「色を付ける」
/// </summary>
public class ColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Header("未選択時の色")] private Color _unselected = Color.gray;
    private Color _originalColor = default;
    private Image _image = default;

    private void Start()
    {
        _image = GetComponent<Image>();
        if (_image == null)
        {
            Debug.LogWarning("Imageコンポーネントがありません。");
        }
        else
        {
            _originalColor = _image.color;
        }

        _image.color = _unselected;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_image != null)
        {
            _image.color = _originalColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_image != null)
        {
            _image.color = _unselected;
        }
    }
}