using FancyScrollView;
using UnityEngine;
using UnityEngine.UI;

internal class Tab : FancyCell<SellItemData, Context>
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Text _message;
    [SerializeField] private Button _button;

    private float _currentPosition;

    private static class AnimatorHash
    {
        public static readonly int Scroll = Animator.StringToHash("ScrollButton");
    }

    public override void Initialize()
    {
        _button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
    }

    public override void UpdateContent(SellItemData itemData)
    {
        _message.text = itemData.Message;
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

    // GameObject が非アクティブになると Animator がリセットされてしまうため
    // 現在位置を保持しておいて OnEnable のタイミングで現在位置を再設定します
    private void OnEnable() => UpdatePosition(_currentPosition);
}