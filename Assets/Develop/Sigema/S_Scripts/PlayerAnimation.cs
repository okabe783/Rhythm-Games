using UnityEngine;

/// <summary>
/// 入力に応じてアニメーションを遷移させる
/// </summary>
public class PlayerAnimation : MonoBehaviour, IPlayerInput
{
    private Animator _animator = default;
    private static readonly int Attack1 = Animator.StringToHash("Attack1");
    private static readonly int Attack2 = Animator.StringToHash("Attack2");
    private static readonly int Attack3 = Animator.StringToHash("Attack3");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Death = Animator.StringToHash("Death");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // todo:Attack1~2をどう決めるかは未定
    public void InputUpper()
    {
        _animator.Play(Attack1);
    }

    public void InputUpperAndLower()
    {
        _animator.Play(Attack2);
    }

    public void InputLower()
    {
        _animator.Play(Attack2);
    }

    // Attack3はロングノーツ用
    // todo: アニメーションがループである必要がある
    public void InputLongPressStart()
    {
        _animator.SetBool(Attack3, true);
    }

    // todo: 「InputLongPressStart」で真にしたものを偽にする
    public void InputLongPressEnd()
    {
        _animator.SetBool(Attack3, false);
    }

    public void PlayHit()
    {
        _animator.Play(Hit);
    }

    public void PlayDeath()
    {
        _animator.Play(Death);
    }
}