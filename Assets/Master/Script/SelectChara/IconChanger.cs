using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// FaceIconをかざしているキャラに応じて切り替える
/// 表示先のImageをOnOff切り替えている
/// </summary>
public class IconChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CharacterDataBaseList _datalist = default;
    [SerializeField, Header("表示先のImage")] private Image _image = default;
    [SerializeField, Header("キャラID")] private int _charaID = default;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_datalist.CharacterDataBase[_charaID].FaceIcon != null)
        {
            _image.enabled = true;
            _image.sprite = _datalist.CharacterDataBase[_charaID].FaceIcon;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_datalist.CharacterDataBase[_charaID].FaceIcon != null)
        {
            _image.enabled = false;
            _image.sprite = null;
        }
    }
}