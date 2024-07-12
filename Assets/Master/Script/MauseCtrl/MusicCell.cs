using FancyScrollView;
using UnityEngine;
using UnityEngine.UI;

public class MusicCell : FancyCell<MusicItemData>
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Text _txtName;

    private float _currentPosition;

    private static class AnimatorHash
    {
        public static readonly int _scroll = Animator.StringToHash("scroll");
    }

    public override void UpdateContent(MusicItemData itemData)
    {
        _txtName.text = itemData._musicName;
    }

    private void OnEnable() => UpdatePosition(_currentPosition);

    //UIの位置を更新してアニメーションを再生
    public override void UpdatePosition(float position)
    {
        _currentPosition = position;
        if (_animator.isActiveAndEnabled)
            _animator.Play(AnimatorHash._scroll, -1, position);
        _animator.speed = 0;
    }
}