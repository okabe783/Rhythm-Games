using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// アウトラインの非表示
/// </summary>
public class OutlineChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Header("アウトラインのマテリアル")]
    private Material _outline = default;

    [SerializeField, Header("対象")] private Image _target = default;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _target.material = _outline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _target.material = null;
    }
}