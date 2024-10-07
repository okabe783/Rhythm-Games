using FancyScrollView;
using UnityEngine;
using UnityEngine.UI;

internal class Tab : FancyCell<SellItemData, Context>
{
    [SerializeField] private Animator _animator;
    private SoundTable _musicID;
    [SerializeField] private Button _button;

    private float _currentPosition;

    private static class AnimatorHash
    {
        public static readonly int Scroll = Animator.StringToHash("ScrollButton");
    }

    public SoundTable MusicID => _musicID;

    public override void UpdateContent(SellItemData itemData)
    {
        _musicID = itemData.SoundTable;
    }

    public override void UpdatePosition(float position)
    {
        _currentPosition = position;

        if (_animator.isActiveAndEnabled)
        {
            _animator.Play(AnimatorHash.Scroll, -1, position);
        }

        _animator.speed = 0;
    }
    
    private void OnEnable() => UpdatePosition(_currentPosition);
}