using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 対象：スクリプトをアタッチしたもの
/// カーソルをかざしたときに「アニメーションを再生する」
/// </summary>
public class AnimChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator _animator = default;
    private static readonly int IsPlay = Animator.StringToHash("IsPlay");

    private void Start()
    {
        if (_animator == null)
        {
            Debug.LogWarning($"{gameObject.name}にAnimatorがアタッチされていません。");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetBool(IsPlay, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetBool(IsPlay, false);
    }
}