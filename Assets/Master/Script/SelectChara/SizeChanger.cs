using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 対象：スクリプトをアタッチしたもの
/// カーソルをかざしたときに「サイズを大きくする」
/// </summary>
public class SizeChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Header("倍率")] private float _magnification = 1.5f;
    private Vector3 _beforeSize = default; // 変化前の大きさ
    private Vector3 _afterSize = default; // 変化後の大きさ

    private void Start()
    {
        _beforeSize = transform.localScale;
        _afterSize = _beforeSize * _magnification;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = _afterSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = _beforeSize;
    }
}