using UnityEngine;

/// <summary>
/// 入力に応じてアニメーションを遷移させる
/// </summary>
public class PlayerAnimation : MonoBehaviour, IPlayerInput
{
    private Animator _animator = default;
    private static readonly int Attack3 = Animator.StringToHash("Attack3");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // todo:Attack1~2をどう決めるかは未定
    public void InputUpper()
    {
        _animator.Play("Attack1");
    }

    public void InputUpperAndLower()
    {
        _animator.Play("Attack2");
    }

    public void InputLower()
    {
        _animator.Play("Attack2");
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

    public void Hit()
    {
        _animator.Play("Hit");
    }

    public void Death()
    { 
        _animator.Play("Death");
    }
}